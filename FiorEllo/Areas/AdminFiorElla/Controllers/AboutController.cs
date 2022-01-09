using System;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    [Authorize]
    public class AboutController : Controller
    {
        private AppDbContext _context { get; }

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Abouts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(About about)
        {
            about.CreaDate = DateTime.Now;
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // public IActionResult Detail(int id)
        // {
        //     return Json(new
        //     {
        //         Id = id
        //     });
        // }

        public async Task<IActionResult> Update(int id)
        {
            var About = await GetAboutById(id);
            return View(About);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, About about)
        {
            var About = await GetAboutById(id);
            About.Title = about.Title;
            About.Head = about.Head;
            About.Image = about.Image;
            About.CreaDate=DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var about = await GetAboutById(id);
            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<About> GetAboutById(int id)
        {
            var About = await _context.Abouts.FindAsync(id);
            return About;
        }
    }
}