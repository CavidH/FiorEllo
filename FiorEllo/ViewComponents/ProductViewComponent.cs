using FiorEllo.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FiorEllo.ViewComponents
{
    public class ProductViewComponent :ViewComponent
    {
        private AppDbContext _context;
        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }
        
    }
}