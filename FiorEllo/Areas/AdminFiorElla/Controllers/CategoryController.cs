using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]

    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ProductCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid) return View();
            bool IsExits = _context.ProductCategories.Any(p => p.Name.ToLower().Trim() == productCategory.Name.ToLower().Trim());
            if (IsExits)
            {
                ModelState.AddModelError("Name", "This category  already exits");
                return View();
            }

            await _context.ProductCategories.AddAsync(productCategory);
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //input uchun def calue
            var category = await _context.ProductCategories.FindAsync(id);
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductCategory productCategory)
        {
            if (!ModelState.IsValid) return View();
            var category = await _context.ProductCategories.Where(p => p.Id == id).FirstOrDefaultAsync();

            category.Name = productCategory.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // [Area("AdminFiorElla")]
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // // public async Task<IActionResult> Update(ProductCategory productCategory)
        // // {
        // // //    if (!ModelState.IsValid) return View();
        // // //     // bool IsExits = _context.ProductCategories.Any(p => p.Name.ToLower().Trim() == productCategory.Name.ToLower().Trim());
        // // //     // if (IsExits)
        // // //     // {
        // // //     //     ModelState.AddModelError("Name", "This category  already exits");
        // // //     //     return View();
        // // //     // }
        // // //     //
        // // //    await _context.ProductCategories.Where(p=>p.i)
        // // // //  await _context.SaveChangesAsync();
        // //
        // //     return RedirectToAction(nameof(Index));
        // // }

        public IActionResult Delete(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
    }
}