using System.Collections.Generic;
using FiorEllo.Models;

namespace FiorEllo.ViewModel
{
    public class BasketProductVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public int StockCount { get; set; }
        public decimal Price { get; set; }
        
    }
}