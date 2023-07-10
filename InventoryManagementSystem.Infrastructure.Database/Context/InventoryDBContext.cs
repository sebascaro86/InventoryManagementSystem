using InventoryManagementSystem.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Infrastructure.Database.Context
{
    /// <summary>
    /// Represents the database context for the inventory management system.
    /// </summary>
    public class InventoryDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryDBContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options)
        { }

        public DbSet<Buy> Buys { get; set; }

        public DbSet<BuyItem> BuyItems { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Configures the relationships between entities in the database context.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance.</param>
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
