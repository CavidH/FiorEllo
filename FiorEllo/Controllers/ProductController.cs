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
            SettingLoadBtn TakeCount =await  _context.SettingLoadBtn.FirstOrDefaultAsync();
            
            
            var model = await _context
                .Products
                .OrderByDescending(product => product.Id)
                .Skip(skip)
                .Take(TakeCount.ProductSkipCount)
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Image)
                .ToListAsync();
            //return Json(model);
            return PartialView("_ProductPartial", model);
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id==null) return NotFound();
            Product dbproduct =await _context.Products.FindAsync(id);
            if (dbproduct == null) return BadRequest();

            List<BasketVM> basket = GetBasket();
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
            List<BasketVM> basket;
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

        public IActionResult Basket()
        {
            return View();
            // return Json(JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]));
        }




 




} }
