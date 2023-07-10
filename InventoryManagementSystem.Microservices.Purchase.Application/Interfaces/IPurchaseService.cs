using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Interfaces
{
    /// <summary>
    /// Represents the interface for purchase service.
    /// </summary>
    public interface IPurchaseService
    {
        /// <summary>
        /// Retrieves all purchases.
        /// </summary>
        /// <returns>A collection of purchase DTOs.</returns>
        Task<ICollection<PurchaseDTO>> GetPurchases();

        /// <summary>
        /// Retrieves a purchase by ID.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase.</param>
        /// <returns>The purchase DTO.</returns>
        Task<PurchaseDTO> GetPurchase(string purchaseId);

        /// <summary>
        /// Registers a new purchase.
        /// </summary>
        /// <param name="purchaseDTO">The register purchase DTO.</param>
        /// <returns>The registered purchase DTO.</returns>
        Task<PurchaseDTO> RegisterPurchase(RegisterPurchaseDTO purchaseDTO);
    }
}
