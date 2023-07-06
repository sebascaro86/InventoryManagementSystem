using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Items()
        {
            return Ok("sebastian");
        }
    }
}
