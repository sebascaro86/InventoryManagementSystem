using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Microservices.Purchase.Domain.CommandHandlers;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;
using InventoryManagementSystem.Microservices.Purchase.Domain.Events;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Domain.CommandHandlers
{
    [TestFixture]
    public class UpdateProductsCommandHandlerTests
    {
        private Mock<IEventBus> _eventBusMock;
        private UpdateProductsCommandHandler _commandHandler;

        [SetUp]
        public void Setup()
        {
            _eventBusMock = new Mock<IEventBus>();
            _commandHandler = new UpdateProductsCommandHandler(_eventBusMock.Object);
        }

        [Test]
        public async Task Handle_WithValidCommand_ShouldPublishUpdatedProductsEvent()
        {
            // Arrange
            var products = new List<UpdateProductCommand>
        {
            new UpdateProductCommand(Guid.NewGuid(), 10),
            new UpdateProductCommand(Guid.NewGuid(), 5)
        };
            var command = new UpdateProductsCommand(products);

            // Act
            var result = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _eventBusMock.Verify(mock => mock.Publish(It.IsAny<UpdatedProductsEvent>()), Times.Once);
        }
    }
}
