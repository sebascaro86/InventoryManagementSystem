using InventoryManagementSystem.Microservices.Purchase.API.Controllers;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.API.Controllers
{
    [TestFixture]
    public class ClientsControllerTests
    {
        private Mock<IClientService> _clientServiceMock;
        private ClientsController _controller;

        [SetUp]
        public void Setup()
        {
            _clientServiceMock = new Mock<IClientService>();
            _controller = new ClientsController(_clientServiceMock.Object);
        }

        [Test]
        public async Task GetClients_ShouldReturnOkResult()
        {
            // Arrange
            var clients = new List<ClientDTO>
        {
            new ClientDTO { Id = Guid.NewGuid(), Name = "Client 1" },
            new ClientDTO { Id = Guid.NewGuid(), Name = "Client 2" },
        };

            _clientServiceMock.Setup(mock => mock.GetClients()).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetClients();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(clients));
        }

        [Test]
        public async Task GetClient_WithValidClientId_ShouldReturnOkResult()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var client = new ClientDTO { Id = clientId, Name = "Client" };

            _clientServiceMock.Setup(mock => mock.GetClient(clientId.ToString())).ReturnsAsync(client);

            // Act
            var result = await _controller.GetClient(clientId.ToString());

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(client));
        }

        [Test]
        public async Task CreateClient_WithValidClientDTO_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var clientDTO = new CreateClientDTO { Name = "New Client" };
            var createdClient = new ClientDTO { Id = Guid.NewGuid(), Name = "New Client" };

            _clientServiceMock.Setup(mock => mock.CreateClient(clientDTO)).ReturnsAsync(createdClient);

            // Act
            var result = await _controller.CreateCustomer(clientDTO);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(ClientsController.GetClient)));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(createdClient));
        }
    }
}
