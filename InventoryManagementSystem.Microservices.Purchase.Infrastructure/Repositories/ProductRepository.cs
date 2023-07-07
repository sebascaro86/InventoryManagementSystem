using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDBContext _dbContext;

        public ProductRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Product>> GetProductsByIds(ICollection<Guid> ids)
        {
            return await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
