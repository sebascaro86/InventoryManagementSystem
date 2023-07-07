using InventoryManagementSystem.Microservices.Purchase.API.Filters;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientService.GetClients());
        }

        [HttpGet("{customerId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> GetClient(string clientId)
        {
            return Ok(await _clientService.GetClient(clientId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateClientDTO clientDTO)
        {
            var createdClient = await _clientService.CreateClient(clientDTO);
            return CreatedAtAction(nameof(GetClient), new { clientID = createdClient.Id}, createdClient);
        }
    }
}
