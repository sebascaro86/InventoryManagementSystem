using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Interfaces
{
    public interface IClientService
    {
        Task<ICollection<ClientDTO>> GetClients();
        Task<ClientDTO> GetClient(string clientId);
        Task<ClientDTO> CreateClient(CreateClientDTO customerDTO);
    }
}
