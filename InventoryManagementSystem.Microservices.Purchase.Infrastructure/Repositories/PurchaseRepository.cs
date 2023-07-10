using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly InventoryDBContext _dbContext;

        public PurchaseRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Buy>> GetBuys()
        {
            return await _dbContext.Buys
                .Include(p => p.Products)
                .Include(p => p.Client).ToListAsync();
        }

        public async Task<Buy> GetBuy(Guid buyId)
        {
            var purchase = await _dbContext.Buys
                .Include(p => p.Products)
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.Id == buyId);

            if (purchase == null)
            {
                throw new NotFoundException($"The purchase with the ID {buyId} does not exist.");
            }

            return purchase;
        }

        public async Task<Buy> CreateBuy(Buy buy)
        {
            _dbContext.Buys.Add(buy);
            await _dbContext.SaveChangesAsync();
            return buy;
        }
    }
}
