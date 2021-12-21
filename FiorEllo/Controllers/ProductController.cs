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
            var Products = await _context
                .Products
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Image)
                .OrderByDescending(product => product.Id)
                .Take(8)
                .ToListAsync();
            return View(Products);
        }

        //public async Task<IActionResult> LoadProduct()
        //{
        //    var model = await _context
        //        .Products
        //        .OrderByDescending(product => product.Id)
        //        .Skip(8)
        //        .Take(8)
        //        .Where(product => product.IsDeleted == false)
        //        .Include(product => product.Image)
        //        .ToListAsync(); 

        //    return PartialView("_ProductPartial", model);
        //}
    }
}
