using InventoryManagementSystem.Microservices.Purchase.API.Filters;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Purchase.API.Controllers
{
    /// <summary>
    /// Represents the controller for managing clients.
    /// </summary>
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="clientService">The service for managing clients.</param>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>A collection of clients.</returns>
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _clientService.GetClients());
        }

        /// <summary>
        /// Gets a specific client by its ID.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The client with the specified ID.</returns>
        [HttpGet("{clientId}")]
        [ValidateGuidIdAttribute("clientId")]
        public async Task<IActionResult> GetClient(string clientId)
        {
            return Ok(await _clientService.GetClient(clientId));
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="clientDTO">The client data to create.</param>
        /// <returns>The created client.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateClientDTO clientDTO)
        {
            var createdClient = await _clientService.CreateClient(clientDTO);
            return CreatedAtAction(nameof(GetClient), new { clientId = createdClient.Id}, createdClient);
        }
    }
}
