using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    public class ProductController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context
                .Products
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Category)
                .Include(product => product.Image));
        }
        public IActionResult Create()
        {
            return View();
        }
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

        public IActionResult Detail(int id)
        {
            return Json(new
            {
                Id=id
            });
        }
        public async Task<IActionResult> Update(int id)
        {
           var product=await _context.Products.Include(p => p.Image).Where(p => p.Id==id).FirstAsync();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,Product product)
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var product=await _context.Products.FindAsync(id);
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
    }
}