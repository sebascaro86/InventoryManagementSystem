using InventoryManagementSystem.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Infrastructure.Database.Context
{
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options)
        { }

        public DbSet<Buy> Buys { get; set; }

        public DbSet<BuyItem> BuyItems { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Buy>()
                .HasOne(o => o.Client)
                .WithMany(bp => bp.Buys)
                .HasForeignKey(bp => bp.ClientId);

            modelBuilder.Entity<Buy>()
                .HasMany(bpi => bpi.Products)
                .WithOne(bp => bp.Buy)
                .HasForeignKey(bp => bp.BuyId);
        }
    }
}
