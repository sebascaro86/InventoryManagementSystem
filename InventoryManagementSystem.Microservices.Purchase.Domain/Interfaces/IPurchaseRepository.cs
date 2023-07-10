using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<ICollection<Buy>> GetBuys();
        Task<Buy> GetBuy(Guid buyId);

        Task<Buy> CreateBuy(Buy buy);
    }
}
