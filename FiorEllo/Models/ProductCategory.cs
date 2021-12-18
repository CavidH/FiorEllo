using System.Collections.Generic;
using System.ComponentModel;

namespace FiorEllo.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
         [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
