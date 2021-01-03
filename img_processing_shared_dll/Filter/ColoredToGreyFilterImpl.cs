using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace img_processing_shared_dll.Filter
{
    //Преобразует в оттенки серого
    public class ColoredToGreyFilterImpl : ZeroNeighborsSharedFilter
    {
        public ColoredToGreyFilterImpl()
        {
            FilterID = 2;
            FilterName = "Colored to grey";
        }

        public override int Filter(int incomingPixel)
        {
            Color c = new Color(incomingPixel);
            byte val = (byte)((c.R + c.G + c.B) / 3);
            return val + (val << 8) + (val << 16) + (0xFF << 24);
        }
    }
}
