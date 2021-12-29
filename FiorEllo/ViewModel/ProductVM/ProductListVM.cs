using System.Collections.Generic;
using FiorEllo.Models;
using Microsoft.AspNetCore.Http;

namespace FiorEllo.ViewModel.ProductVM
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public List<IFormFile> Files { get; set; }

    }
}