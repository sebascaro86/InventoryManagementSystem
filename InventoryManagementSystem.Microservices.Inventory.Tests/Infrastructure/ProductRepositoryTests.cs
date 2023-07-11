using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Infrastructure.Database.Context;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;
using InventoryManagementSystem.Microservices.Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Inventory.Tests.Infrastructure
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private DbContextOptions<InventoryDBContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<InventoryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Test]
        public async Task GetAll_WithExistingProducts_ShouldReturnAllProducts()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var products = new List<Product>
                {
                    new Product { Id = Guid.NewGuid(), Name = "Product 1", InInventory = 10 },
                    new Product { Id = Guid.NewGuid(), Name = "Product 2", InInventory = 5 },
                    new Product { Id = Guid.NewGuid(), Name = "Product 3", InInventory = 15 }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.GetAll();

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.Count(), Is.EqualTo(products.Count));
                CollectionAssert.AreEquivalent(products, result);
            }
        }

        [Test]
        public async Task Get_WithExistingProductId_ShouldReturnProduct()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var productId = Guid.NewGuid();
                var product = new Product { Id = productId, Name = "Product 1", InInventory = 10 };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.Get(productId);

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.Id, Is.EqualTo(productId));
                Assert.That(result.Name, Is.EqualTo(product.Name));
                Assert.That(result.InInventory, Is.EqualTo(product.InInventory));
            }
        }

        [Test]
        public void Get_WithNonExistingProductId_ShouldThrowNotFoundException()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var nonExistingProductId = Guid.NewGuid();

                //Act
                var repository = new ProductRepository(context);

                // Act & Assert
                Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(nonExistingProductId));
            }
        }

        [Test]
        public async Task Create_WithValidProduct_ShouldCreateProduct()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var product = new Product { Id = Guid.NewGuid(), Name = "Product 1", InInventory = 10 };

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.Create(product);

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result, Is.EqualTo(product));

                var createdProduct = await context.Products.FindAsync(product.Id);
                Assert.IsNotNull(createdProduct);
                Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
                Assert.That(createdProduct.InInventory, Is.EqualTo(product.InInventory));
            }
        }

        [Test]
        public async Task Update_WithExistingProduct_ShouldUpdateProduct()
        {
            var productId = Guid.NewGuid();

            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var product = new Product { Id = productId, Name = "Product 1", InInventory = 10 };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
            }

            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                var updatedProduct = new Product { Id = productId, Name = "Updated Product", InInventory = 20 };

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.Update(updatedProduct);

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result, Is.EqualTo(updatedProduct));

                var updatedProductFromDb = await context.Products.FindAsync(productId);
                Assert.IsNotNull(updatedProductFromDb);
                Assert.That(updatedProductFromDb.Name, Is.EqualTo(updatedProduct.Name));
                Assert.That(updatedProductFromDb.InInventory, Is.EqualTo(updatedProduct.InInventory));
            }
        }

        [Test]
        public async Task Delete_WithExistingProduct_ShouldDeleteProduct()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var productId = Guid.NewGuid();
                var product = new Product { Id = productId, Name = "Product 1", InInventory = 10 };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.Delete(productId);

                // Assert
                Assert.IsTrue(result);

                var deletedProduct = await context.Products.FindAsync(productId);
                Assert.IsNull(deletedProduct);
            }
        }

        [Test]
        public async Task Delete_WithNonExistingProduct_ShouldReturnFalse()
        {
            using (var context = new InventoryDBContext(_dbContextOptions))
            {
                // Arrange
                var nonExistingProductId = Guid.NewGuid();

                // Act
                var repository = new ProductRepository(context);
                var result = await repository.Delete(nonExistingProductId);

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}
