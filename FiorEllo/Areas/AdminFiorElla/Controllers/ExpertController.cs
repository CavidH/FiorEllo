using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    public class ExpertController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public ExpertController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.Experts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expert expert)
        {
            await _context.Experts.AddAsync(expert);
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

        public async Task<IActionResult>  Update(int id)
        {
            return View(await GetExpertById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Update(int id,Expert expert)
        {
            var Expert = await GetExpertById(id);
            Expert.Name = expert.Name;
            Expert.SurName = expert.SurName;
            Expert.Position = expert.Position;
            Expert.Photo = expert.Photo;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            _context.Remove(await GetExpertById(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Expert> GetExpertById(int id)
        {
            return await _context.Experts.FindAsync(id);
        }
    }
}