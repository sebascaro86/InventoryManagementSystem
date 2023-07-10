using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Interfaces
{
    // <summary>
    /// Represents the interface for client service.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Retrieves all clients.
        /// </summary>
        /// <returns>A collection of client DTOs.</returns>
        Task<ICollection<ClientDTO>> GetClients();

        /// <summary>
        /// Retrieves a client by ID.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The client DTO.</returns>
        Task<ClientDTO> GetClient(string clientId);

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="customerDTO">The create client DTO.</param>
        /// <returns>The created client DTO.</returns>
        Task<ClientDTO> CreateClient(CreateClientDTO customerDTO);
    }
}
