using FiorEllo.Models;
using Microsoft.EntityFrameworkCore;

namespace FiorEllo.DAL
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<SliderIntro> Sliders { get; set; }
        public DbSet<IntroTxt> Introtxt { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
