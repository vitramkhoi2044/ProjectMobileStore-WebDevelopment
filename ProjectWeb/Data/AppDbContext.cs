using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectWeb.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Mobiles> Mobiles { get; set; } = null;
        public DbSet<BrandCategories> BrandCategories { get; set; } = null;
        public DbSet<OrderMobiles> OrderMobiles { get; set; } = null;
        public DbSet<Orders> Orders { get; set; } = null;
        public DbSet<Promotions> Promotions { get; set; } = null;

    }
}
