using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace img_processing_shared_dll
{
    //Базовый класс клиента 
    public class PeerBaseContainer
    {
        //Клиент соединения 
        public  TcpClient Client { get; protected set; }
        //Его GUID
        public Guid Guid { get; set; }

        //Буфер для чтения/записи
        public byte[] Buffer { get; protected set; }

        public PeerBaseContainer(Guid guid, TcpClient client)
        {
            Guid = guid;
            Client = client;
            Buffer = new byte[9000000];
            Client.ReceiveTimeout = 1000000;
        }

        protected PeerBaseContainer() 
        {
            Buffer = new byte[9000000];
            Client = new TcpClient();
            Client.ReceiveTimeout = 1000000;
        }

        //отрпавка сообщения 
        //отправляем длину сообщения
        //затем само сообщение
        public void SendMessage(ImageProcessingMessage msg)
        {
            Client.GetStream().Write(BitConverter.GetBytes(msg.TotalLength), 0, 4);
            Client.GetStream().Write(msg.AsBytes, 0, msg.TotalLength - 16);
        }
    }
}
