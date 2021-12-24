using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiorEllo.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name invalid"),MaxLength(25)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
