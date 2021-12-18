using FiorEllo.DAL;
using FiorEllo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FiorEllo.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm
            {
                sliderIntros = await _context.Sliders.ToListAsync(),
                IntroTxt = await _context.Introtxt.FirstOrDefaultAsync(),
                Cards = await _context.Cards.ToListAsync(),

            };
            return View(homeVm);
        }

    }
}
