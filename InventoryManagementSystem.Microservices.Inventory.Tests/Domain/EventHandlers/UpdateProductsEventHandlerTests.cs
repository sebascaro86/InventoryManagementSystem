using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Inventory.Domain.EventHandlers;
using InventoryManagementSystem.Microservices.Inventory.Domain.Events;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Inventory.Tests.Domain.EventHandlers
{
    [TestFixture]
    public class UpdateProductsEventHandlerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateProductsEventHandler _updateProductsEventHandler;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _updateProductsEventHandler = new UpdateProductsEventHandler(_productRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_WithUpdatedProducts_ShouldUpdateProductsInInventory()
        {
            // Arrange
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();
            var updateProductCommand1 = new UpdateProductCommand(productId1, 3);
            var updateProductCommand2 = new UpdateProductCommand(productId2, 5);
            var updatedProductsEvent = new UpdatedProductsEvent(new List<UpdateProductCommand> { updateProductCommand1, updateProductCommand2 });

            var product1 = new Product { Id = productId1, InInventory = 10 };
            var product2 = new Product { Id = productId2, InInventory = 8 };

            _productRepositoryMock.Setup(mock => mock.Get(productId1)).ReturnsAsync(product1);
            _productRepositoryMock.Setup(mock => mock.Get(productId2)).ReturnsAsync(product2);

            // Act
            await _updateProductsEventHandler.Handle(updatedProductsEvent);

            // Assert
            _productRepositoryMock.Verify(mock => mock.Update(product1), Times.Once);
            _productRepositoryMock.Verify(mock => mock.Update(product2), Times.Once);
            Assert.That(product1.InInventory, Is.EqualTo(7));
            Assert.That(product2.InInventory, Is.EqualTo(3));
        }
    }
}
