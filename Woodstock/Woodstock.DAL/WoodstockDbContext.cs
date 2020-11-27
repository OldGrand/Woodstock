using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Woodstock.DAL.Entities;

namespace Woodstock.DAL
{
    public class WoodstockDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<BodyMaterial> BodyMaterials { get; set; }
        public DbSet<GlassMaterial> GlassMaterials { get; set; }
        public DbSet<Mechanism> Mechanisms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Strap> Straps { get; set; }
        public DbSet<StrapMaterial> StrapMaterials { get; set; }
        public DbSet<Watch> Watches { get; set; }  
        public DbSet<OrderHistory> OrdersHistory { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderWatchLink> OrderWatchLinks { get; set; }

        public WoodstockDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Watch>().Property(p => p.Gender)
                .HasConversion<string>();

            builder.Entity<Mechanism>().Property(p => p.MechanismType)
                .HasConversion<string>();

            builder.Entity<Manufacturer>().Property(p => p.Country)
                .HasConversion<string>();

            base.OnModelCreating(builder);
        }
    }
}
