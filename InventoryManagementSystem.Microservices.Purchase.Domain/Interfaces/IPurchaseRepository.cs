using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    /// <summary>
    /// Represents a repository for managing purchases.
    /// </summary>
    public interface IPurchaseRepository
    {
        /// <summary>
        /// Retrieves all the buys.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of buys.</returns>
        Task<ICollection<Buy>> GetBuys();

        /// <summary>
        /// Retrieves a buy by its ID.
        /// </summary>
        /// <param name="buyId">The ID of the buy to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the buy.</returns>
        Task<Buy> GetBuy(Guid buyId);

        /// <summary>
        /// Creates a new buy.
        /// </summary>
        /// <param name="buy">The buy to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created buy.</returns>
        Task<Buy> CreateBuy(Buy buy);
    }
}
