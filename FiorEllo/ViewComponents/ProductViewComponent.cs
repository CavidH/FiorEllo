using System.Linq;
using System.Threading.Tasks;
using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private AppDbContext _context;

        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int takeCount)
        {
            var Model = await _context
                .Products
                .Where(product => product.IsDeleted == false)
                .Include(product => product.Category)
                .Include(product => product.Image)
                .OrderByDescending(product => product.Id) //en yeni  productlari goturmek uchun. sondan evvele  
                .Take(takeCount) //artiq data gelisnin qabaqini aliriq
                .ToListAsync();
            return View(await Task.FromResult(Model));
        }
    }
}