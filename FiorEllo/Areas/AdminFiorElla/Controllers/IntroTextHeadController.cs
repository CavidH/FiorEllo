using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    public class IntroTextHeadController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public IntroTextHeadController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var intro=await _context.Introtxt.FirstAsync();
            return View(intro);
        }
        
        public IActionResult Update(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
         
    }
}