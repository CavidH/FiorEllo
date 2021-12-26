using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    public class ProductController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [Area("AdminFiorElla")]

        public IActionResult Index()
        {
            return View(_context
                .Products
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Category)
                .Include(product => product.Image));
        }
        [Area("AdminFiorElla")]
        public IActionResult Create()
        {
            return View();
        }
        [Area("AdminFiorElla")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View();
            bool IsExits = _context.Products.Any(p => p.Name.ToLower().Trim() == product.Name.ToLower().Trim());
            bool CategoryIdIsExits = _context.ProductCategories.Any(p => p.Id == product.CategoryId);
            if (IsExits)
            {
                ModelState.AddModelError("Name", "This category  already exits");
                return View();
            }

            if (!CategoryIdIsExits)
            {
                ModelState.AddModelError("Name", " category not found  "); 
            }
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Area("AdminFiorElla")]
        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id=id
            });
        }
        [Area("AdminFiorElla")]
        public IActionResult Update(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
        [Area("AdminFiorElla")]
        public async Task<IActionResult> Delete(int id)
        {
            var product=await _context.Products.FindAsync(id);
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
    }
}