using InventoryManagementSystem.Domain.Core.Models;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<ICollection<Client>> GetClients();
        Task<Client> GetClient(Guid customerId);
        Task<Client> CreateClient(Client customer);
    }
}
