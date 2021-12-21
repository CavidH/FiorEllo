using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.ViewModel
{
    public class ProductController : Controller
    {
        public AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.productscount = _context
                .Products
                .Where(product => product.IsDeleted == false)
                .Count();
            var Products = await _context
                .Products
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Image)
                .OrderByDescending(product => product.Id)
                .Take(8)
                .ToListAsync();
            return View(Products);
        }

        public async Task<IActionResult> LoadProduct(int skip)
        {
             
            var model = await _context
                .Products
                .OrderByDescending(product => product.Id)
                .Skip(skip)
                .Take(8)
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Image)
                .ToListAsync();
            //return Json(model);
            return PartialView("_ProductPartial", model);
        }
    }
}
