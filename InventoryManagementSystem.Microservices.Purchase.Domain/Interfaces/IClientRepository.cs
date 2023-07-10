using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    /// <summary>
    /// Represents a repository for managing clients.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Retrieves all clients.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of clients.</returns>
        Task<ICollection<Client>> GetClients();

        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="customerId">The ID of the client to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the retrieved client.</returns>
        Task<Client> GetClient(Guid customerId);

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="customer">The client to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created client.</returns>
        Task<Client> CreateClient(Client customer);
    }
}
