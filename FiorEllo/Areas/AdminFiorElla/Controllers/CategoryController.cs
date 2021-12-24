﻿using FiorEllo.DAL;
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