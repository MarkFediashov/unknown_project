using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_processing_shared_dll.Filter
{
    //интерфейс для фильра с 0 соседей (matrix[1,1] -> pixel)
    public interface IZeroNeighborsFilter : IFilter
    {
        int Filter(int incomingPixel);
    }
}
