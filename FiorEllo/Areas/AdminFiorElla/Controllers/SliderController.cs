using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using FiorEllo.Services.Utilities;
using FiorEllo.ViewModel.Slider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    [Authorize]
    public class SliderController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        private string _erorrMessage;

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
            ViewData["count"] = GetEmptySliderCount();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MultipleSliderVM sliderVm)
        {
            #region Single File Upload

            // if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            // if (!slide.Photo.CheckFileType("image/"))
            // {
            //     ModelState.AddModelError("Photo", "file  should be  image type ");
            //     return View();
            // }
            //
            // if (!slide.Photo.CheckFileSize(300))
            // {
            //     ModelState.AddModelError("Photo", "file size must be less than 200kb");
            //     return View();
            // }
            //
            // string filename = await slide.Photo.SaveFileAsync(_env.WebRootPath, "Assets", "img");
            // slide.Image = filename;
            // await _context.Sliders.AddAsync(slide);
            // await _context.SaveChangesAsync();

            #endregion

            int EmptySlide = GetEmptySliderCount();

            if (sliderVm.Photos.Count > EmptySlide)
            {
                ModelState.AddModelError("Photos", $"hal hazirda {EmptySlide} eded slide yukleye bilersiz  **Limit 5 slide");
                return View();
            }

            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();
            if (!ChechkImageValid(sliderVm.Photos))
            {
                ModelState.AddModelError("Photos", _erorrMessage);
                return View();
            }

            foreach (var photo in sliderVm.Photos)
            {
                string filename = await photo.SaveFileAsync(_env.WebRootPath, "Assets", "img");
                await _context.Sliders.AddAsync(new SliderIntro {Image = filename});
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private int GetEmptySliderCount()
        {
            return 5 - _context.Sliders.Count();
        }

        private bool ChechkImageValid(List<IFormFile> photos)
        {
            foreach (var photo in photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    _erorrMessage = $"{photo.FileName} must be  image type ";
                    return false;
                }

                if (!photo.CheckFileSize(300))
                {
                    _erorrMessage = $"{photo.FileName} size must be less than 200kb";
                    return false;
                }
            }

            return true;
        }


        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id = id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var slide = await _context.Sliders.FindAsync(id);
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SliderIntro slider)
        {
            if (id == null) return NotFound();
            if (!ModelState.IsValid) return View();
            var slide = await _context.Sliders.FindAsync(id);
            if (slider.Photo == null) return NotFound();
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
                return RedirectToAction(nameof(Index));
            Helper.RemoveFile(_env.WebRootPath, slide.Image, "Assets", "img");

            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "file  should be  image type ");
                return View();
            }

            if (!slider.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "file size must be less than 200kb");
                return View();
            }

            string filename = await slider.Photo.SaveFileAsync(_env.WebRootPath, "Assets", "img");
            slide.Image = filename;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var slide = await _context.Sliders.FindAsync(id);
            // if (slide == null) return NotFound();
            Helper.RemoveFile(_env.WebRootPath, slide.Image, "Assets", "img");
            _context.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}