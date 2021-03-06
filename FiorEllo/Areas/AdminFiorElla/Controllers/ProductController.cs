using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using FiorEllo.Models;
using FiorEllo.ViewModel;
using FiorEllo.ViewModel.ProductVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.Areas.AdminFiorElla.Controllers
{
    [Area("AdminFiorElla")]
    [Authorize]
    public class ProductController : Controller
    {
        // GET
        private AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var products = await _context
                .Products
                .Where(product => product.IsDeleted == false)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .Include(product => product.Category)
                .Include(product => product.Image)
                .ToListAsync();
            var ProductVMs = getProductList(products);
            var PageCount = getPageCount(take);
            Paginate<ProductListVM> ProductPaginate = new Paginate<ProductListVM>(ProductVMs, page, PageCount);
            ViewBag.PageCount = getPageCount(take);
           // return Json(ProductPaginate);
              return View(ProductPaginate);
        }

        private int getPageCount(int take)
        {
            var productCount = _context.Products.Where(p => p.IsDeleted == false).Count();
            return (int) Math.Ceiling(((decimal) productCount / take));
        }

        private List<ProductListVM> getProductList(List<Product> products)
        {
            List<ProductListVM> productLis = new List<ProductListVM>();
            foreach (var product in products)
            {
                var productVM = new ProductListVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = product.Count,
                    CategoryName = product.Category.Name,
                    ProductImage = product.Image.Where(p => p.IsMain == true).FirstOrDefault().Image
                };
                productLis.Add(productVM);
            }

            return productLis;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _context.ProductCategories.Where(p => p.IsDeleted == false).ToListAsync();
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
                Id = id
            });
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.Include(p => p.Image).Where(p => p.Id == id).FirstAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Product product)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}