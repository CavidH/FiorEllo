using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorEllo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
    }
}
