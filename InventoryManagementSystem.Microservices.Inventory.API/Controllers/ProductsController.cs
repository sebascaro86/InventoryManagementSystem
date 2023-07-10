using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Inventory.API.Filters;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerController"/> class.
        /// </summary>
        /// <param name="ownerService">The service for managing owners.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProducts()); 
        }

        [HttpGet("{productId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            return Ok(await _productService.GetProduct(productId));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{productId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> UpdateProduct(string productId, UpdateProductDTO product)
        {
            return Ok(await _productService.UpdateProduct(productId, product));
        }

        [HttpDelete("{productId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
