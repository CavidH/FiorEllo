using System.Linq;
using FiorEllo.DAL;
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
            return Json(new
            {
                Name="Create"
            });
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
        public IActionResult Delete(int id)
        {
            return Json(new
            {
                Id = id
            });
        }
    }
}