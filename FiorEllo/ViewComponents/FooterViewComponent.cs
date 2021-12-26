using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
             
            return View();
        }
    }
}