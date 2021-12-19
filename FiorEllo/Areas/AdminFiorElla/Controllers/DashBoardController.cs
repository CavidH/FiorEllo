using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
