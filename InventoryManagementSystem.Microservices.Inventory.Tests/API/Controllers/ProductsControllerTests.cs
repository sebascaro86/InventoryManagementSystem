using InventoryManagementSystem.Microservices.Inventory.API.Controllers;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Inventory.Tests.API.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [Test]
        public async Task GetProducts_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<ProductDTO>
        {
            new ProductDTO { Id = Guid.NewGuid(), Name = "Product 1" },
            new ProductDTO { Id = Guid.NewGuid(), Name = "Product 2" }
        };
            _productServiceMock.Setup(x => x.GetProducts()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(products));
        }

        [Test]
        public async Task GetProduct_WithValidProductId_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            var product = new ProductDTO { Id = Guid.Parse(productId), Name = "Product 1" };
            _productServiceMock.Setup(x => x.GetProduct(productId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(productId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(product));
        }

        [Test]
        public async Task AddProduct_WithValidProduct_ShouldReturnCreatedProduct()
        {
            // Arrange
            var product = new CreateProductDTO { Name = "New Product" };
            var createdProduct = new ProductDTO { Id = Guid.NewGuid(), Name = "New Product" };
            _productServiceMock.Setup(x => x.CreateProduct(product)).ReturnsAsync(createdProduct);

            // Act
            var result = await _controller.AddProduct(product);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetProduct"));
            Assert.That(createdAtActionResult.Value, Is.EqualTo(createdProduct));
        }

        [Test]
        public async Task UpdateProduct_WithValidProductIdAndProduct_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            var updatedProduct = new UpdateProductDTO { Name = "Updated Product" };
            var updatedProductDto = new ProductDTO { Id = Guid.Parse(productId), Name = "Updated Product" };
            _productServiceMock.Setup(x => x.UpdateProduct(productId, updatedProduct)).ReturnsAsync(updatedProductDto);

            // Act
            var result = await _controller.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(updatedProductDto));
        }

        [Test]
        public async Task DeleteProduct_WithValidProductId_ShouldReturnNoContent()
        {
            // Arrange
            var productId = "1";
            _productServiceMock.Setup(x => x.DeleteProduct(productId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteProduct_WithInvalidProductId_ShouldReturnNotFound()
        {
            // Arrange
            var productId = "1";
            _productServiceMock.Setup(x => x.DeleteProduct(productId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
