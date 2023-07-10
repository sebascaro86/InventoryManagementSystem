using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;

namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    /// <summary>
    /// Represents a purchase data transfer object.
    /// </summary>
    public class PurchaseDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<ProductPurchaseDTO> Products { get; set; }
        public ClientDTO Client { get; set; }
    }
}
