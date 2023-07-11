using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class PurchaseRepositoryTests
    {
        private DbContextOptions<InventoryDBContext> _dbContextOptions;
        private InventoryDBContext _dbContext;
        private PurchaseRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InventoryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new InventoryDBContext(_dbContextOptions);
            _repository = new PurchaseRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetBuys_WhenCalled_ShouldReturnAllPurchasesWithProductsAndClient()
        {
            // Arrange
            var client = new Client { Id = Guid.NewGuid(), Name = "Client 1", IdType = "CC", Identification = 121212 };
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1" },
                new Product { Id = Guid.NewGuid(), Name = "Product 2" },
                new Product { Id = Guid.NewGuid(), Name = "Product 3" }
            };

            var purchases = new List<Buy>
            {
                new Buy { Id = Guid.NewGuid(), Date = DateTime.Now, Client = client },
                new Buy { Id = Guid.NewGuid(), Date = DateTime.Now, Client = client }
            };

            _dbContext.Clients.Add(client);
            _dbContext.Products.AddRange(products);
            _dbContext.Buys.AddRange(purchases);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetBuys();

            // Assert
            Assert.That(result.Count, Is.EqualTo(purchases.Count));
            foreach (var purchase in result)
            {
                Assert.That(purchase.Client, Is.EqualTo(client));
            }
        }

        [Test]
        public async Task GetBuy_WhenCalledWithExistingId_ShouldReturnMatchingPurchaseWithProductsAndClient()
        {
            // Arrange
            var client = new Client { Id = Guid.NewGuid(), Name = "Client 1", IdType = "CC", Identification = 121212 };
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1" },
                new Product { Id = Guid.NewGuid(), Name = "Product 2" },
                new Product { Id = Guid.NewGuid(), Name = "Product 3" }
            };

            var purchase = new Buy { Id = Guid.NewGuid(), Date = DateTime.Now, Client = client  };

            _dbContext.Clients.Add(client);
            _dbContext.Products.AddRange(products);
            _dbContext.Buys.Add(purchase);

            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetBuy(purchase.Id);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Client, Is.EqualTo(client));
        }

        [Test]
        public void GetBuy_WhenCalledWithNonExistingId_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _repository.GetBuy(nonExistingId);
            });
        }

        [Test]
        public async Task CreateBuy_WhenCalled_ShouldCreateAndReturnNewPurchase()
        {
            // Arrange
            var client = new Client { Id = Guid.NewGuid(), Name = "Client 1", IdType = "CC", Identification = 121212 };
            var purchase = new Buy { Id = Guid.NewGuid(), Date = DateTime.Now, Client = client };

            // Act
            var result = await _repository.CreateBuy(purchase);

            // Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(purchase));
            Assert.That(result.Id, Is.EqualTo(purchase.Id));
            Assert.That(result.Date, Is.EqualTo(purchase.Date));
            Assert.That(result.Client, Is.EqualTo(client));
        }
    }
}
