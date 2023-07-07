using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProductsByIds(ICollection<Guid> ids);
    }
}
