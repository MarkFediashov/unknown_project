using System;
using System.Collections.Generic;
using System.Linq;

namespace img_processing_shared_dll.Filter
{
    //Класс-обёртка для фильтров
    public abstract class ZeroNeighborsSharedFilter : IZeroNeighborsFilter
    {
        //номер фильтра
        public int FilterID { get; set; }
        //имя фильтра
        public string FilterName { get; set; }

        //абстрактный метод перобразования 
        public abstract int Filter(int incomingPixel);

        public override string ToString()
        {
            return FilterName;
        }

        //статический метод для получения фильтра по ID
        public static ZeroNeighborsSharedFilter GetFilterByID(int id)
        {
            return AllFilters.Where((ZeroNeighborsSharedFilter f) => { return f.FilterID == id; }).First();
        }

        //статический метод для получения всех фильтров 
        public static IReadOnlyList<ZeroNeighborsSharedFilter> GetAllFilters()
        {
            return AllFilters.AsReadOnly();
        }

        //все возможные фильтры
        private static readonly List<ZeroNeighborsSharedFilter> AllFilters = new List<ZeroNeighborsSharedFilter>() { new ColoredToGreyFilterImpl(), new EqualFilterImpl() };
    }
}
