using FashKartWebsite.DataAccesLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FashKartWebsite.DataAccesLayer
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { 
        
        
             
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Header> Homes { get; set; }
    }
}
