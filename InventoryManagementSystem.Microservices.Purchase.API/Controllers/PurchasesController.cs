using InventoryManagementSystem.Microservices.Purchase.API.Filters;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Controllers
{
    /// <summary>
    /// Represents the controller for managing purchases.
    /// </summary>
    [Route("/api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasesController"/> class.
        /// </summary>
        /// <param name="purchaseService">The service for managing purchases.</param>
        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Gets the purchase history.
        /// </summary>
        /// <returns>A collection of purchases.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPurchaseHistory()
        {
            return Ok(await _purchaseService.GetPurchases());
        }

        /// <summary>
        /// Gets a specific purchase by its ID.
        /// </summary>
        /// <param name="purchaseId">The ID of the purchase.</param>
        /// <returns>The purchase with the specified ID.</returns>
        [HttpGet("{purchaseId}")]
        [ValidateGuidIdAttribute("purchaseId")]
        public async Task<IActionResult> GetPurchase(string purchaseId)
        {
            return Ok(await _purchaseService.GetPurchase(purchaseId));
        }

        /// <summary>
        /// Registers a new purchase.
        /// </summary>
        /// <param name="purchaseDTO">The purchase data to register.</param>
        /// <returns>The registered purchase.</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterPurchase(RegisterPurchaseDTO purchaseDTO)
        {
            var registeredPurchase = await _purchaseService.RegisterPurchase(purchaseDTO);
            return CreatedAtAction(nameof(GetPurchase), new { purchaseId = registeredPurchase.Id }, registeredPurchase);
        }
    }
}
