using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces
{
    /// <summary>
    /// Represents the interface for the product repository.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Gets a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        Task<Product> Get(Guid productId);

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A collection of products.</returns>
        Task<IEnumerable<Product>> GetAll();

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        Task<Product> Create(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>The updated product.</returns>
        Task<Product> Update(Product product);

        /// <summary>
        /// Deletes a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>A boolean indicating whether the product was successfully deleted.</returns>
        Task<bool> Delete(Guid productId);
    }
}
