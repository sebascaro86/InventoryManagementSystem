using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    /// <summary>
    /// Represents a repository for managing products.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves products by their IDs.
        /// </summary>
        /// <param name="ids">The IDs of the products to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of products.</returns>
        Task<ICollection<Product>> GetProductsByIds(ICollection<Guid> ids);
    }
}
