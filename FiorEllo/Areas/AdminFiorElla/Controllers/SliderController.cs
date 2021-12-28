using System;
using System.IO;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using FiorEllo.Services.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "file  should be  image type ");
                return View();
            }

            if (!slide.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "file size must be less than 200kb");
                return View();
            }
            string filename = await slide.Photo.SaveFileAsync(_env.WebRootPath, "Assets", "img");
            slide.Image = filename;
            await _context.Sliders.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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