using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using InventoryManagementSystem.Microservices.Inventory.Application.Services;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace InventoryManagementSystem.Microservices.Inventory.Tests.Application.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetProducts_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", InInventory = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", InInventory = 5 }
            };

            var productDTOs = new List<ProductDTO>
            {
                new ProductDTO { Id = Guid.NewGuid(), Name = "Product 1", InInventory = 10 },
                new ProductDTO { Id = Guid.NewGuid(), Name = "Product 2", InInventory = 5 }
            };

            _productRepositoryMock.Setup(mock => mock.GetAll()).ReturnsAsync(products);
            _mapperMock.Setup(mock => mock.Map<List<ProductDTO>>(products)).Returns(productDTOs);

            // Act
            var result = await _productService.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ProductDTO>>(result);
        }

        [Test]
        public async Task GetProduct_WithValidProductId_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Product 1", InInventory = 10 };
            var productDTO = new ProductDTO { Id = productId, Name = "Product 1", InInventory = 10 };

            _productRepositoryMock.Setup(mock => mock.Get(productId)).ReturnsAsync(product);
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(productDTO);

            // Act
            var result = await _productService.GetProduct(productId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProductDTO>(result);
            Assert.That(result.Id, Is.EqualTo(productId));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.InInventory, Is.EqualTo(product.InInventory));
        }

        [Test]
        public async Task CreateProduct_WithValidProductDTO_ShouldCreateProduct()
        {
            // Arrange
            var productDTO = new CreateProductDTO { Name = "Product 1", InInventory = 10 };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product 1", InInventory = 10 };
            var productDTOAfterCreate = new ProductDTO { Id = product.Id, Name = "Product 1", InInventory = 10 };

            _mapperMock.Setup(mock => mock.Map<Product>(productDTO)).Returns(product);
            _productRepositoryMock.Setup(mock => mock.Create(product)).ReturnsAsync(product);
            _mapperMock.Setup(mock => mock.Map<ProductDTO>(product)).Returns(productDTOAfterCreate);

            // Act
            var result = await _productService.CreateProduct(productDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ProductDTO>(result);
            Assert.That(result.Id, Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(productDTO.Name));
            Assert.That(result.InInventory, Is.EqualTo(productDTO.InInventory));
        }

        [Test]
        public async Task UpdateProduct_WithValidProductIdAndProductDTO_ShouldUpdateProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var productDTO = new UpdateProductDTO { Name = "Updated Product", InInventory = 20 };
            var product = new Product { Id = productId, Name = "Product 1", InInventory = 10, Min = 10, Max = 30 };

            var updatedProduct = new Product { Id = productId, Name = "Updated Product", InInventory = 20, Min = 10, Max = 30 };
            var updatedProductDTO = new ProductDTO { Id = productId, Name = "Updated Product", InInventory = 20 };

            _productRepositoryMock.Setup(mock => mock.Get(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(mock => mock.Update(product)).ReturnsAsync(updatedProduct);

            _productRepositoryMock.Setup(mock => mock.Get(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(mock => mock.Update(updatedProduct)).ReturnsAsync(updatedProduct);

            _mapperMock.Setup(mock => mock.Map<ProductDTO>(updatedProduct)).Returns(updatedProductDTO);

            // Act
            var result = await _productService.UpdateProduct(productId.ToString(), productDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(updatedProduct.Id));
            Assert.That(result.Name, Is.EqualTo(updatedProduct.Name));
            Assert.That(result.InInventory, Is.EqualTo(updatedProduct.InInventory));
            Assert.That(result.Enabled, Is.EqualTo(updatedProduct.Enabled));
        }

        [Test]
        public async Task DeleteProduct_WithValidProductId_ShouldDeleteProduct()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            _productRepositoryMock.Setup(mock => mock.Delete(Guid.Parse(productId))).ReturnsAsync(true);

            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
