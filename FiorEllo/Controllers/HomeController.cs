using System.Linq;
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
            HttpContext.Session.Set("name","Cavid");
            HomeVm homeVm = new HomeVm
            {
                sliderIntros = await _context.Sliders.ToListAsync(),
                IntroTxt = await _context.Introtxt.FirstOrDefaultAsync(),
                ProductCategories = await _context
                    .ProductCategories
                    .Where(productcategory => productcategory.IsDeleted == false)
                    .ToListAsync(),
                Products = await _context
                    .Products
                    .Where(product => product.IsDeleted == false)
                    .Include(product => product.Category)
                    .Include(product => product.Image)
                    .OrderByDescending(product => product.Id)//en yeni  productlari goturmek uchun. sondan evvele  
                    .Take(8) //artiq data gelisnin qabaqini aliriq
                    .ToListAsync(),
               NewsAbout = await _context.News.FirstOrDefaultAsync(),
               Experts = await _context.Experts.ToListAsync(),
               Abouts = await _context.Abouts.ToListAsync(),
               ExpertSlides = await _context.ExpertSlides.ToListAsync().ConfigureAwait(false)


               //Cards = await _context.Cards.ToListAsync(),

            };
            return View(homeVm);
        }

    }
}
