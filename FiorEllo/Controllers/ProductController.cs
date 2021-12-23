using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.EntityFrameworkCore;
using FiorEllo.Models;
using Newtonsoft.Json;

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
            // var Products = await _context
            //     .Products
            //     .Where(product => product.IsDeleted == false)
            //     .Include(product => product.Image)
            //     .OrderByDescending(product => product.Id)
            //     .Take(8)
            //     .ToListAsync();
            // return View(Products);
            return View();
        }

        public async Task<IActionResult> LoadProduct(int skip)
        {
            var TakeCount = _context.Setting.Where(p => p.Key == "TakeCount").ToList().FirstOrDefault();
            var count = Convert.ToInt32(TakeCount.Value);



            var model = await _context
                .Products
                .OrderByDescending(product => product.Id)
                .Skip(skip)
                .Take(count)
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Image)
                .ToListAsync();
            //return Json(model);
            return PartialView("_ProductPartial", model);
        }

        public List<BasketVM> basket;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
        
            if (id==null) return NotFound();
            Product dbproduct =await _context.Products.FindAsync(id);
            if (dbproduct == null) return BadRequest();

             basket = GetBasket();
            UpdateBasket(basket,(int)id);
            
            return RedirectToAction("Index","Home");

        }

        private void UpdateBasket(List<BasketVM> basket,int Id)
        {
            BasketVM BasketProduct = basket.Find(p => p.Id ==Id);
            if (BasketProduct==null)
            {
                basket.Add(new BasketVM
                {
                    Id =Id,
                    Count = 1
                });
            }
            else
            {
                BasketProduct.Count+=1;
            }
            
         
            Response.Cookies.Append("basket",JsonConvert.SerializeObject(basket));
        }

        private List<BasketVM> GetBasket()
        {
            
            if (Request.Cookies["basket"]!=null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }
       
        public async Task<IActionResult> Basket()
        {
            List<BasketProductVm> basketProductVms = new List<BasketProductVm>();

            foreach (var item in GetBasket())
            {
                var product = await _context
                    .Products
                    .Include(p => p.Image)
                    .FirstOrDefaultAsync(p=>p.Id==item.Id);
                
                basketProductVms.Add(new BasketProductVm
                {
                     Id = item.Id,
                     Title = product.Name,
                     Count = item.Count,
                     Image = product.Image.Where(p=>p.IsMain==true).FirstOrDefault().Image,
                     Price = product.Price,
                     StockCount = product.Count
                });

            }
             

            return View(basketProductVms);
            // return Json(JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]));
        }

        public IActionResult RemoveBasketItem(int id)
        {
            var basketList = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            var product = basketList.Find(pr => pr.Id == id);
            basketList.Remove(product);
            Response.Cookies.Append("basket",JsonConvert.SerializeObject(basketList));
            return RedirectToAction("Basket", "Product");
        }

} }
