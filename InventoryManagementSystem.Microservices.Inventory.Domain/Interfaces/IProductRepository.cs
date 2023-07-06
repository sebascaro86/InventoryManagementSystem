using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Get(Guid productId);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(Guid productId);
    }
}
