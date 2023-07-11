using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Purchase.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Infrastructure.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private DbContextOptions<InventoryDBContext> _dbContextOptions;
        private InventoryDBContext _dbContext;
        private ProductRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InventoryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new InventoryDBContext(_dbContextOptions);
            _repository = new ProductRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetProductsByIds_WhenCalledWithExistingIds_ShouldReturnMatchingProducts()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1" },
            new Product { Id = Guid.NewGuid(), Name = "Product 2" },
            new Product { Id = Guid.NewGuid(), Name = "Product 3" }
        };
            _dbContext.Products.AddRange(products);
            await _dbContext.SaveChangesAsync();
            var ids = products.Select(p => p.Id).ToList();

            // Act
            var result = await _repository.GetProductsByIds(ids);

            // Assert
            Assert.That(result.Count, Is.EqualTo(products.Count));
        }

        [Test]
        public async Task GetProductsByIds_WhenCalledWithNonExistingIds_ShouldReturnEmptyCollection()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1" },
            new Product { Id = Guid.NewGuid(), Name = "Product 2" },
            new Product { Id = Guid.NewGuid(), Name = "Product 3" }
        };
            _dbContext.Products.AddRange(products);
            await _dbContext.SaveChangesAsync();
            var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            // Act
            var result = await _repository.GetProductsByIds(ids);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
