using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<ICollection<PurchaseDTO>> GetPurchases();
        Task<PurchaseDTO> GetPurchase(string purchaseId);
        Task<PurchaseDTO> RegisterPurchase(RegisterPurchaseDTO purchaseDTO);
    }
}
