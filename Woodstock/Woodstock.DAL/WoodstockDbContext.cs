using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Woodstock.DAL.Entities;

namespace Woodstock.DAL
{
    public class WoodstockDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<BodyMaterial> BodyMaterials { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<GlassMaterial> GlassMaterials { get; set; }
        public DbSet<Mechanism> Mechanisms { get; set; }
        public DbSet<MechanismType> MechanismTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Strap> Straps { get; set; }
        public DbSet<StrapMaterial> StrapMaterials { get; set; }
        public DbSet<Watch> Watches { get; set; }   

        public WoodstockDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartWatchLink>().HasKey(_ => new { _.ShoppingCartId, _.WatchId });
            builder.Entity<OrderWatchLink>().HasKey(_ => new { _.OrderId, _.WatchId });

            builder.Entity<CartWatchLink>()
                .HasOne(_ => _.Watch)
                .WithMany(_ => _.CartWatchLinks)
                .HasForeignKey(_ => _.WatchId);

            builder.Entity<CartWatchLink>()
                .HasOne(_ => _.ShoppingCart)
                .WithMany(_ => _.CartWatchLinks)
                .HasForeignKey(_ => _.ShoppingCartId);

            builder.Entity<OrderWatchLink>()
                .HasOne(_ => _.Watch)
                .WithMany(_ => _.OrderWatchLinks)
                .HasForeignKey(_ => _.WatchId);

            builder.Entity<OrderWatchLink>()
                .HasOne(_ => _.Order)
                .WithMany(_ => _.OrderWatchLinks)
                .HasForeignKey(_ => _.OrderId);

            base.OnModelCreating(builder);
        }
    }
}
