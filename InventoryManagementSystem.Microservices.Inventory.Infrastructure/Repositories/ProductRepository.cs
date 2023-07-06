using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManagementSystem.Microservices.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDBContext _dbContext;

        public ProductRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> Get(Guid productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new NotFoundException($"The product with the ID {productId} does not exist.");
            }

            return product;
        }

        public async Task<Product> Create(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> Delete(Guid productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }        
    }
}
