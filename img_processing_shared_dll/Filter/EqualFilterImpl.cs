using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_processing_shared_dll.Filter
{
    //Эквивалентный фильтр (служит для теста)
    class EqualFilterImpl : ZeroNeighborsSharedFilter
    {
        public EqualFilterImpl()
        {
            FilterName = "Equal";
            FilterID = 3;
        }
        public override int Filter(int incomingPixel)
        {
            return incomingPixel;
        }

    }
}
