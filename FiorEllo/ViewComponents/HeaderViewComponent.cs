using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
         
        public async Task<IViewComponentResult> InvokeAsync()
        {
             
            return View();
        }
    }
}