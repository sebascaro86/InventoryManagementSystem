using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;

namespace InventoryManagementSystem.Microservices.Inventory.Application.Interfaces
{
    /// <summary>
    /// Represents the interface for the product service.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A collection of product DTOs.</returns>
        Task<IEnumerable<ProductDTO>> GetProducts();

        /// <summary>
        /// Gets a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The product DTO with the specified ID.</returns>
        Task<ProductDTO> GetProduct(string productId);

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDTO">The product DTO containing the product data.</param>
        /// <returns>The created product DTO.</returns>
        Task<ProductDTO> CreateProduct(CreateProductDTO productDTO);

        /// <summary>
        /// Updates a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="productDTO">The product DTO containing the updated product data.</param>
        /// <returns>The updated product DTO.</returns>
        Task<ProductDTO> UpdateProduct(string productId, UpdateProductDTO productDTO);

        /// <summary>
        /// Deletes a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>A boolean indicating whether the product was successfully deleted.</returns>
        Task<bool> DeleteProduct(string productId);
    }
}
