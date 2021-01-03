using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_process_master
{
    //делегат, вызываемый после обработки изображения 
    delegate void OnImageProcessed(int[] image);
    //интерфейс для обработки изображения
    interface IProcessing
    {
        void ProcessImage(Bitmap image, int filterID, OnImageProcessed onImageProcessed);
    }
}
