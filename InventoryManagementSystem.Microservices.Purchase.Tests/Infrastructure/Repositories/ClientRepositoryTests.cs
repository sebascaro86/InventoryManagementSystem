using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class ClientRepositoryTests
    {
        private DbContextOptions<InventoryDBContext> _dbContextOptions;
        private InventoryDBContext _dbContext;
        private ClientRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InventoryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new InventoryDBContext(_dbContextOptions);
            _repository = new ClientRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetClients_WhenCalled_ShouldReturnAllClients()
        {
            // Arrange
            var clients = new List<Client>
        {
            new Client { Id = Guid.NewGuid(), Name = "Client 1", IdType = "CC", Identification = 15151515 },
            new Client { Id = Guid.NewGuid(), Name = "Client 2", IdType = "CC", Identification = 12121211 },
            new Client { Id = Guid.NewGuid(), Name = "Client 3", IdType = "CC", Identification = 15151515 }
        };
            _dbContext.Clients.AddRange(clients);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetClients();

            // Assert
            Assert.That(result.Count, Is.EqualTo(clients.Count));
        }

        [Test]
        public async Task GetClient_WithValidClientId_ShouldReturnClient()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, Name = "Client", IdType = "CC", Identification = 15151515 };
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetClient(clientId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(clientId));
            Assert.That(result.Name, Is.EqualTo(client.Name));
        }

        [Test]
        public void GetClient_WithInvalidClientId_ShouldThrowNotFoundException()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.GetClient(clientId));
        }

        [Test]
        public async Task CreateClient_WithValidClient_ShouldCreateClient()
        {
            // Arrange
            var client = new Client { Id = Guid.NewGuid(), Name = "Client", IdType = "CC", Identification = 15151515 };

            // Act
            var result = await _repository.CreateClient(client);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(client.Id));
            Assert.That(result.Name, Is.EqualTo(client.Name));
        }
    }
}
