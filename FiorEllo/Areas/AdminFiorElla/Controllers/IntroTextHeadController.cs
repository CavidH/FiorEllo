using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class IntroTextHeadController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public IntroTextHeadController(AppDbContext context)
        {
            _context = context;
        }
        [Area("AdminFiorElla")]
        public async Task<IActionResult> Index()
        {
            var intro=await _context.Introtxt.FirstAsync();
            return View(intro);
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