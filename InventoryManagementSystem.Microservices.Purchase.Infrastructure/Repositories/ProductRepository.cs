using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ProductRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<ICollection<Product>> GetProductsByIds(ICollection<Guid> ids)
        {
            return await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
