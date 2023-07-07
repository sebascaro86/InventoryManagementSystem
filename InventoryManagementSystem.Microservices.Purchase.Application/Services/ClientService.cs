using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ClientDTO>> GetClients()
        {
            List<Client> clients = (await _clientRepository.GetClients()).ToList();
            return _mapper.Map<List<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> GetClient(string clientId)
        {
            Client client = await _clientRepository.GetClient(Guid.Parse(clientId));
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> CreateClient(CreateClientDTO clientDto)
        {
            Client client = _mapper.Map<Client>(clientDto);
            await _clientRepository.CreateClient(client);
            return _mapper.Map<ClientDTO>(client);
        }
    }
}
