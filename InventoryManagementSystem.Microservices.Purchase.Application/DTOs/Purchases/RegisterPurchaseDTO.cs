namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    /// <summary>
    /// Represents a register purchase data transfer object.
    /// </summary>
    public class RegisterPurchaseDTO
    {
        public ICollection<ProductPurchaseDTO> Products { get; set; }

        [GuidValidation(ErrorMessage = "The clientId must be a valid Guid")]
        public string ClientId { get; set; }
    }
}
