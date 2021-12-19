using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("AdminFiorElla")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
