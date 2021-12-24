using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class CategoryController : Controller
    {
        private AppDbContext _context { get; }

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [Area("AdminFiorElla")]
        public IActionResult Index()
        {
            return View(_context.ProductCategories);
        }

        [Area("AdminFiorElla")]
        public IActionResult Create()
        {
            return View();
        }

        [Area("AdminFiorElla")]
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

        [Area("AdminFiorElla")]
        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id = id
            });
        }

        [Area("AdminFiorElla")]
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Update(int id,string name)
        {
            var c = _context.ProductCategories.Where(p => p.Id == id);
            return Json(new
            {
                id=id,
                name=name
            });
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

        [Area("AdminFiorElla")]
        public IActionResult Delete(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
    }
}