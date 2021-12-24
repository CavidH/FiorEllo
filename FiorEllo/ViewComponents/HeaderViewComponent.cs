using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FiorEllo.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public List<BasketVM> basket;
         
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            ViewBag.count =GetBasket().Count;
            return View();
        }
        private List<BasketVM> GetBasket()
        {
            
            if (Request.Cookies["basket"]!=null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }
    }
}