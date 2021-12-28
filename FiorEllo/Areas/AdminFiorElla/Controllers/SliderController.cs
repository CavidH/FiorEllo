using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderIntro slide)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();


            return Content(slide.Photo.Length+" "+slide.Photo.FileName);
            // await _context.Sliders.AddAsync(slide);
            // await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id = id
            });
        }

        public async Task<IActionResult> Update(int id)
        {
            var slide = await _context.Sliders.FindAsync(id);
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SliderIntro slider)
        {
            var slide = await _context.Sliders.FindAsync(id);
            slide.Image = slider.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var slide = await _context.Sliders.FindAsync(id);
            _context.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}