using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_processing_shared_dll
{
    //вспомогательная структура 
    public struct Color
    {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }

        public Color(int v)
        {
            R = (byte)(v & 0xFF);
            G = (byte)((v >> 8) & 0xFF);
            B = (byte)((v >> 16) & 0xFF);
        }
    }
}
