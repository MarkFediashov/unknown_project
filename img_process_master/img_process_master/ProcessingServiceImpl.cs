using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using img_processing_shared_dll;

namespace img_process_master
{
    class ProcessingServiceImpl : IProcessing
    {
        //класс с конфигурацией клиента который сервер ???
        private static class Config
        {
            public static readonly int Port = 30687;
            public static readonly IPAddress Ip = IPAddress.Parse("127.0.0.1");
        }
        //мьютекс на вставку в список
        private readonly Mutex mutex;
        //мьютекс на декремент поля
        private readonly Mutex onDecrement;

        //временное поле, хранит счётчик оставшихся кусков
        public int PartsRemained { get; private set; }

        //слушатель входящих соединений
        protected TcpListener Listener { get; private set; }
        //список всех peer'ов
        private List<PeerBaseContainer> Workers { get; set; }

        //временный буфер для картинки
        private int[] ArrayTempBuffer { get; set; }
        //смещение (оно же кусок работы для всех)
        private int Offset { get; set; }
        //временная переменная, хранящая делегат для вызова
        OnImageProcessed onImageProcessed;

        public ProcessingServiceImpl()
        {
            Listener = new TcpListener(Config.Ip, Config.Port);
            Workers = new List<PeerBaseContainer>();
            mutex = new Mutex();
            onDecrement = new Mutex();
            Startup();
        }

        //начинаем слушать в отдельной нити
        private async void Startup()
        {
            Listener.Start();
            while (true)
            {
                TcpClient client = await Listener.AcceptTcpClientAsync();
                if (mutex.WaitOne())
                {
                    PeerBaseContainer newSlave = new PeerBaseContainer(Guid.NewGuid(), client);
                    
                    byte[] bytes = newSlave.Guid.ToByteArray();
                    newSlave.Client.GetStream().Write(bytes, 0, bytes.Length);

                    Workers.Add(newSlave);

                    mutex.ReleaseMutex();
                }
                
            }
        }
        //получить массив интов по картинке
        private int[] GetImageArray(Bitmap image)
        {
            int[] array = new int[image.Width * image.Height];
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    array[i * image.Width + j] = image.GetPixel(j, i).ToArgb();
                }
            }
            return array;
        }
        //обработать картинку
        public void ProcessImage(Bitmap image, int filterID, OnImageProcessed onImageProcessed)
        {
            this.onImageProcessed = onImageProcessed;
            if (Workers.Count == 0)
            {
                MessageBox.Show("App must contains at least 1 peer", "Error");
            }
            else
            {
                //высчитываем смещение, сколько частей будем ждать и тд
                int[] imageArray = GetImageArray(image);
                ArrayTempBuffer = imageArray;
                int workerOffset = imageArray.Length / Workers.Count;
                PartsRemained = Workers.Count;
                Offset = workerOffset;
                int counter = 0;
                // для каждого пира
                foreach(PeerBaseContainer peer in Workers)
                {
                    //создаём сообщение
                    ImageProcessingMessage message = new ImageProcessingMessage(counter++, filterID, image.Height, image.Width, workerOffset, imageArray);
                    //асинхронно отправляем его
                    Task.Factory.StartNew(()=>peer.SendMessage(message));
                    //асинхронно ожидаем ответа
                    peer.Client.GetStream().BeginRead(peer.Buffer, 0, 4, OnReadFromNetworkStream, peer);
                }
            }

        }

        // по получении ответа 
        private void OnReadFromNetworkStream(IAsyncResult result)
        {
            PeerBaseContainer peer = result.AsyncState as PeerBaseContainer;
            int msgLength = BitConverter.ToInt32(peer.Buffer, 0);

            peer.Client.GetStream().Read(peer.Buffer, 0, msgLength);
            //читаем обработанное сообщение
            ImageProcessingMessage msg = new ImageProcessingMessage(peer.Buffer, msgLength);
            
            //присваиваем значения в соответствии с workID и смещением
            for(int i = 0; i < msg.ImageArray.Length; i++)
            {
                ArrayTempBuffer[i + (msg.WorkID * Offset)] = msg.ImageArray[i];
            }
            //декремент переменной оставшихся частей
            if (onDecrement.WaitOne())
            {
                PartsRemained--;
                //если это последняя часть
                if (PartsRemained == 0)
                {
                    //вызываем делегат
                    onImageProcessed(ArrayTempBuffer);
                }
                onDecrement.ReleaseMutex();
            }
            //завершаем чтение
            peer.Client.GetStream().EndRead(result);
        }
    }
}
