using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Services
{
    /// <summary>
    /// Represents the client service implementation.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientRepository">The client repository.</param>
        /// <param name="mapper">The mapper.</param>
        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<ICollection<ClientDTO>> GetClients()
        {
            List<Client> clients = (await _clientRepository.GetClients()).ToList();
            return _mapper.Map<List<ClientDTO>>(clients);
        }

        /// <inheritdoc />
        public async Task<ClientDTO> GetClient(string clientId)
        {
            Client client = await _clientRepository.GetClient(Guid.Parse(clientId));
            return _mapper.Map<ClientDTO>(client);
        }

        /// <inheritdoc />
        public async Task<ClientDTO> CreateClient(CreateClientDTO clientDto)
        {
            Client client = _mapper.Map<Client>(clientDto);
            await _clientRepository.CreateClient(client);
            return _mapper.Map<ClientDTO>(client);
        }
    }
}
