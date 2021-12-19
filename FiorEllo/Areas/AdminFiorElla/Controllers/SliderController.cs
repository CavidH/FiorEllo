using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc; 

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        [Area("AdminFiorElla")]
        public IActionResult Index()
        {
            

            return View(_context.Sliders);
        }
        [Area("AdminFiorElla")]
        public IActionResult Create()
        {
            return Json(new
            {
                Name = "Create"
            });
        }
        [Area("AdminFiorElla")]
        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
        [Area("AdminFiorElla")]
        public IActionResult Update(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
        [Area("AdminFiorElla")]
        public IActionResult Delete(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
    }
}
