using AutoMapper;
using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Services;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Purchase.Tests.Application.Services
{
    [TestFixture]
    public class PurchaseServiceTests
    {
        private Mock<IPurchaseRepository> _purchaseRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IClientRepository> _clientRepositoryMock;
        private Mock<IEventBus> _eventBusMock;
        private Mock<IMapper> _mapperMock;
        private PurchaseService _purchaseService;

        [SetUp]
        public void Setup()
        {
            _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _eventBusMock = new Mock<IEventBus>();
            _mapperMock = new Mock<IMapper>();
            _purchaseService = new PurchaseService(
                _purchaseRepositoryMock.Object,
                _productRepositoryMock.Object,
                _clientRepositoryMock.Object,
                _eventBusMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task GetPurchases_ShouldReturnAllPurchases()
        {
            // Arrange
            var purchases = new List<Buy>
        {
            new Buy { Id = Guid.NewGuid(), Date = DateTime.Now },
            new Buy { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-1) }
        };
            var purchaseDTOs = new List<PurchaseDTO>
        {
            new PurchaseDTO { Id = purchases[0].Id, Date = purchases[0].Date },
            new PurchaseDTO { Id = purchases[1].Id, Date = purchases[1].Date }
        };

            _purchaseRepositoryMock.Setup(mock => mock.GetBuys()).ReturnsAsync(purchases);
            _mapperMock.Setup(mock => mock.Map<List<PurchaseDTO>>(purchases)).Returns(purchaseDTOs);

            // Act
            var result = await _purchaseService.GetPurchases();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(purchaseDTOs, result.ToList());
        }

        [Test]
        public async Task GetPurchase_WithValidPurchaseId_ShouldReturnPurchase()
        {
            // Arrange
            var purchaseId = Guid.NewGuid();
            var purchase = new Buy { Id = purchaseId, Date = DateTime.Now };
            var purchaseDTO = new PurchaseDTO { Id = purchaseId, Date = purchase.Date };

            _purchaseRepositoryMock.Setup(mock => mock.GetBuy(purchaseId)).ReturnsAsync(purchase);
            _mapperMock.Setup(mock => mock.Map<PurchaseDTO>(purchase)).Returns(purchaseDTO);

            // Act
            var result = await _purchaseService.GetPurchase(purchaseId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(purchaseDTO.Id));
            Assert.That(result.Date, Is.EqualTo(purchaseDTO.Date));
        }

        [Test]
        public async Task RegisterPurchase_WithValidPurchaseDto_ShouldCreatePurchase()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var createPurchaseDto = new RegisterPurchaseDTO
            {
                ClientId = clientId.ToString(),
                Products = new List<ProductPurchaseDTO>
            {
                new ProductPurchaseDTO { ProductId = productId.ToString(), Quantity = 2 }
            }
            };
            var client = new Client { Id = clientId, Name = "Client 1" };
            var product = new Product { Id = productId, Name = "Product 1", InInventory = 5, Min = 1, Max = 10 };
            var purchase = new Buy { Id = Guid.NewGuid(), Date = DateTime.Now, Products = new List<BuyItem> { new BuyItem { ProductId = productId, Quantity = 2 } } };
            var purchaseDTO = new PurchaseDTO { Id = purchase.Id, Date = purchase.Date };

            _clientRepositoryMock.Setup(mock => mock.GetClient(clientId)).ReturnsAsync(client);
            _productRepositoryMock.Setup(mock => mock.GetProductsByIds(It.IsAny<List<Guid>>())).ReturnsAsync(new List<Product> { product });
            _mapperMock.Setup(mock => mock.Map<Buy>(createPurchaseDto)).Returns(purchase);
            _purchaseRepositoryMock.Setup(mock => mock.CreateBuy(purchase)).ReturnsAsync(purchase);
            _mapperMock.Setup(mock => mock.Map<PurchaseDTO>(purchase)).Returns(purchaseDTO);

            // Act
            var result = await _purchaseService.RegisterPurchase(createPurchaseDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(purchaseDTO.Id));
            Assert.That(result.Date, Is.EqualTo(purchaseDTO.Date));
            _productRepositoryMock.Verify(mock => mock.GetProductsByIds(It.IsAny<List<Guid>>()), Times.Once);
            _eventBusMock.Verify(mock => mock.SendCommand(It.IsAny<UpdateProductsCommand>()), Times.Once);
        }
    }
}
