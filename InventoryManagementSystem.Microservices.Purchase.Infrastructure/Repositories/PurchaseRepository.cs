using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing purchases.
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly InventoryDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PurchaseRepository(InventoryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<ICollection<Buy>> GetBuys()
        {
            return await _dbContext.Buys
                .Include(p => p.Products)
                .Include(p => p.Client).ToListAsync();
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task<Buy> CreateBuy(Buy buy)
        {
            _dbContext.Buys.Add(buy);
            await _dbContext.SaveChangesAsync();
            return buy;
        }
    }
}
