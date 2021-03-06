using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    [Authorize]
    public class NewsController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            return View(await getNews());
        }
        public async Task<IActionResult> Update()
        {
            return View(await getNews());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(News news)
        {
            var newsDb =  await getNews();
            newsDb.Title = news.Title;
            newsDb.Content = news.Content;
            newsDb.Cover = news.Cover;
            await _context.SaveChangesAsync(); 
            
            return RedirectToAction(nameof(Index));
        }
        private  async Task<News> getNews()
        {
            return await _context.News.FirstAsync();
        }
        
    }
}