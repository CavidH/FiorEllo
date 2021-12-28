using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiorEllo.ViewModel.Slider
{
    public class MultipleSliderVM
    {
        public int İd { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}