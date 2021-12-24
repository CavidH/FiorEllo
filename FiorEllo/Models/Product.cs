using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FiorEllo.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,MaxLength(25)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<ProductImage> Image { get; set; }
    }
}
