using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;

namespace InventoryManagementSystem.Microservices.Inventory.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(string productId);
        Task<ProductDTO> CreateProduct(CreateProductDTO productDTO);
        Task<ProductDTO> UpdateProduct(string productId, UpdateProductDTO productDTO);
        Task<bool> DeleteProduct(string productId);
    }
}
