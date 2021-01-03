using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_processing_shared_dll
{
    //Сообщение для обработчика (peer'а)
    public class ImageProcessingMessage
    {
        //идентификатор части изображения
        public int WorkID { get; set; }
        //Ширина и высота
        public int ImgWidth { get; set; }
        public int ImgHeigth { get; set; }
        //идентификатор фильтра
        public int FilterID { get; set; }
        //массив пикселей
        public int[] ImageArray { get; set; }

        //общая длина сообщения (в байтах)
        public int TotalLength
        {
            get
            {
                return 16 + ImageArray.Length * 4;
            }
        }

        //превратить сообщение в байты
        public byte[] AsBytes
        {
            get
            {
                byte[] resultArr = new byte[TotalLength];
                byte[] wIdArr = BitConverter.GetBytes(WorkID);
                byte[] imWArr = BitConverter.GetBytes(ImgWidth);
                byte[] imHArr = BitConverter.GetBytes(ImgHeigth);
                byte[] fIdArr = BitConverter.GetBytes(FilterID);

                wIdArr.CopyTo(resultArr, 0);
                imWArr.CopyTo(resultArr, 4);
                imHArr.CopyTo(resultArr, 8);
                fIdArr.CopyTo(resultArr, 12);
                Buffer.BlockCopy(ImageArray, 0, resultArr, 16, ImageArray.Length * 4);

                return resultArr;
            }
        }

        //конструктор для создания сообщение (от клиентского приложения к хостам)
        public ImageProcessingMessage(int workID, int filterID, int H, int W, int offset, int[] array) 
        {
            WorkID = workID;
            FilterID = filterID;
            ImgHeigth = H;
            ImgWidth = W;
            ImageArray = new int[offset];
            Buffer.BlockCopy(array, workID * offset * 4, ImageArray, 0, offset * 4);
        }

        //конструктор с десериализацией (парсим байты в поля)
        public ImageProcessingMessage(byte[] arr, int length)
        {
            WorkID = BitConverter.ToInt32(arr, 0);
            ImgWidth = BitConverter.ToInt32(arr, 4);
            ImgHeigth = BitConverter.ToInt32(arr, 8);
            FilterID = BitConverter.ToInt32(arr, 12);
            ImageArray = new int[(length - 16)/4];
            Buffer.BlockCopy(arr, 16, ImageArray, 0, (length - 16));
        }
    }
}
