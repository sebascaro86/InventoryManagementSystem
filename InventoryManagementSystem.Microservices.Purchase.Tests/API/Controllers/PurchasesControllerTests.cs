using InventoryManagementSystem.Microservices.Purchase.API.Controllers;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.API.Controllers
{
    [TestFixture]
    public class PurchasesControllerTests
    {
        private Mock<IPurchaseService> _purchaseServiceMock;
        private PurchasesController _controller;

        [SetUp]
        public void Setup()
        {
            _purchaseServiceMock = new Mock<IPurchaseService>();
            _controller = new PurchasesController(_purchaseServiceMock.Object);
        }

        [Test]
        public async Task GetPurchaseHistory_ShouldReturnOkResult()
        {
            // Arrange
            var purchases = new List<PurchaseDTO>
        {
            new PurchaseDTO { Id = Guid.NewGuid(), Date = DateTime.Now },
            new PurchaseDTO { Id = Guid.NewGuid(), Date = DateTime.Now },
        };

            _purchaseServiceMock.Setup(mock => mock.GetPurchases()).ReturnsAsync(purchases);

            // Act
            var result = await _controller.GetPurchaseHistory();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(purchases));
        }

        [Test]
        public async Task GetPurchase_WithValidPurchaseId_ShouldReturnOkResult()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var purchase = new PurchaseDTO { Id = purchaseId, Date = DateTime.Now };

            _purchaseServiceMock.Setup(mock => mock.GetPurchase(purchaseId.ToString())).ReturnsAsync(purchase);

            // Act
            var result = await _controller.GetPurchase(purchaseId.ToString());

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(purchase));
        }

        [Test]
        public async Task RegisterPurchase_WithValidPurchaseDTO_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var purchaseDTO = new RegisterPurchaseDTO();
            var registeredPurchase = new PurchaseDTO { Id = Guid.NewGuid(), Date = DateTime.Now };

            _purchaseServiceMock.Setup(mock => mock.RegisterPurchase(purchaseDTO)).ReturnsAsync(registeredPurchase);

            // Act
            var result = await _controller.RegisterPurchase(purchaseDTO);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(createdAtActionResult.ActionName, Is.EqualTo(nameof(PurchasesController.GetPurchase)));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(registeredPurchase));
        }
    }
}
