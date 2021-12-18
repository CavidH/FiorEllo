using System.Collections.Generic;
using System.ComponentModel;


namespace FiorEllo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
         [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<ProductImage> Image { get; set; }
    }
}
