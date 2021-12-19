using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class IntroTextHead : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public IntroTextHead(AppDbContext context)
        {
            _context = context;
        }
        [Area("AdminFiorElla")]
        public IActionResult Index()
        {
            return View(_context.Introtxt);
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