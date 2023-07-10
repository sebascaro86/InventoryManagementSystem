namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    public class RegisterPurchaseDTO
    {
        public ICollection<ProductPurchaseDTO> Products { get; set; }
        public string ClientId { get; set; }
    }
}
