using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Services;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Application.Services
{
    [TestFixture]
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private ClientService _clientService;

        [SetUp]
        public void Setup()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mapperMock = new Mock<IMapper>();
            _clientService = new ClientService(_clientRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetClients_ShouldReturnAllClients()
        {
            // Arrange
            var clients = new List<Client>
        {
            new Client { Id = Guid.NewGuid(), Name = "Client 1" },
            new Client { Id = Guid.NewGuid(), Name = "Client 2" }
        };
            var clientDTOs = new List<ClientDTO>
        {
            new ClientDTO { Id = clients[0].Id, Name = clients[0].Name },
            new ClientDTO { Id = clients[1].Id, Name = clients[1].Name }
        };

            _clientRepositoryMock.Setup(mock => mock.GetClients()).ReturnsAsync(clients);
            _mapperMock.Setup(mock => mock.Map<List<ClientDTO>>(clients)).Returns(clientDTOs);

            // Act
            var result = await _clientService.GetClients();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(clientDTOs, result.ToList());
        }

        [Test]
        public async Task GetClient_WithValidClientId_ShouldReturnClient()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, Name = "Client 1" };
            var clientDTO = new ClientDTO { Id = clientId, Name = client.Name };

            _clientRepositoryMock.Setup(mock => mock.GetClient(clientId)).ReturnsAsync(client);
            _mapperMock.Setup(mock => mock.Map<ClientDTO>(client)).Returns(clientDTO);

            // Act
            var result = await _clientService.GetClient(clientId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(clientDTO.Id));
            Assert.That(result.Name, Is.EqualTo(clientDTO.Name));
        }

        [Test]
        public async Task CreateClient_WithValidClientDto_ShouldCreateClient()
        {
            // Arrange
            var createClientDto = new CreateClientDTO { Name = "New Client" };
            var client = new Client { Id = Guid.NewGuid(), Name = createClientDto.Name };
            var clientDTO = new ClientDTO { Id = client.Id, Name = client.Name };

            _mapperMock.Setup(mock => mock.Map<Client>(createClientDto)).Returns(client);
            _clientRepositoryMock.Setup(mock => mock.CreateClient(client)).ReturnsAsync(client);
            _mapperMock.Setup(mock => mock.Map<ClientDTO>(client)).Returns(clientDTO);

            // Act
            var result = await _clientService.CreateClient(createClientDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(clientDTO.Id));
            Assert.That(result.Name, Is.EqualTo(clientDTO.Name));
        }
    }
}
