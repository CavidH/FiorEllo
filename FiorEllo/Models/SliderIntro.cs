using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FiorEllo.Models
{
    public class SliderIntro
    {
        public int Id { get; set; }
      
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
