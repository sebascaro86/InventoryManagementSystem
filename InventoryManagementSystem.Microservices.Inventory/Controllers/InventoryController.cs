using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Inventory.API.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Items()
        {
            return Ok("sebastian");
        }
    }
}
