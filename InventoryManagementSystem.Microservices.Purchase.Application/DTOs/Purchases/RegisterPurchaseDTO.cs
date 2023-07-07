namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    public class RegisterPurchaseDTO
    {
        public List<ProductPurchaseDTO> Products { get; set; }
        public string CustomerId { get; set; }
    }
}
