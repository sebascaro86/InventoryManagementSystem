using InventoryManagementSystem.Microservices.Purchase.API.Filters;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Controllers
{
    [Route("/api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchaseHistory()
        {
            return Ok(await _purchaseService.GetPurchases());
        }

        [HttpGet("{purchaseId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> GetPurchase(string purchaseId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPurchase(RegisterPurchaseDTO purchaseDTO)
        {
            var registeredPurchase = await _purchaseService.RegisterPurchase(purchaseDTO);
            return CreatedAtAction(nameof(GetPurchase), new { purchaseId = registeredPurchase.Id }, registeredPurchase);
        }

        
    }
}
