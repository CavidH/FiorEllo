using System.Collections.Generic;

namespace FiorEllo.ViewModel
{
    public class Paginate<T>
    {
        public Paginate(List<T> items, int currentPage, int pageCount)
        {
            Items = items;
            CurrentPage = currentPage;
            PageCount = pageCount;
        }

        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}