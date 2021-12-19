using System;
using FiorEllo.DAL;
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
    }
}
