namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    /// <summary>
    /// Represents a product purchase data transfer object.
    /// </summary>
    public class ProductPurchaseDTO
    {
        [GuidValidation(ErrorMessage = "The productId must be a valid Guid")]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
