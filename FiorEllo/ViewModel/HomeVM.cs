using System.Collections.Generic;
using FiorEllo.Models;

namespace FiorEllo.ViewModel
{
    public class HomeVm
    {
        public List<SliderIntro> sliderIntros { get; set; }
        public IntroTxt IntroTxt { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Product> Products { get; set; }
        public News NewsAbout { get; set; }
        public List<Expert> Experts { get; set; }
        public List<About> Abouts{ get; set; }

        //public List<Card> Cards { get; set; }
    }
}
