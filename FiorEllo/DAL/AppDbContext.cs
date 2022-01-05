using FiorEllo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SliderIntro> Sliders { get; set; }

        public DbSet<IntroTxt> Introtxt { get; set; }

        //public DbSet<Card> Cards { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ExpertSlide> ExpertSlides { get; set; }
        public DbSet<Setting> Setting { get; set; }
    }
}