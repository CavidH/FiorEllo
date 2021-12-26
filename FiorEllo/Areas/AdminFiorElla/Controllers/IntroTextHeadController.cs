using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            return View(await getIntro());
        }
        
        public  async Task<IActionResult> Update()
        {
            return View(await getIntro());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Update(IntroTxt introTxt)
        {
            var intro = await getIntro();
            intro.Content = introTxt.Content;
            intro.Head = introTxt.Head;
            intro.Logo = introTxt.Logo;
            await  _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private  async Task<IntroTxt> getIntro()
        {
             return await _context.Introtxt.FirstAsync();
        }
         
    }
}