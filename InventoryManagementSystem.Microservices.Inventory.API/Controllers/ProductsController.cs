using InventoryManagementSystem.Microservices.Inventory.API.Filters;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Microservices.Inventory.API.Controllers
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The service for managing products.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>An action result containing the products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProducts()); 
        }

        /// <summary>
        /// Gets a specific product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>An action result containing the product.</returns>
        [HttpGet("{productId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            return Ok(await _productService.GetProduct(productId));
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>An action result representing the created product.</returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Updates a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>An action result representing the updated product.</returns>
        [HttpPut("{productId}")]
        [ValidateGuidIdAttribute("productId")]
        public async Task<IActionResult> UpdateProduct(string productId, UpdateProductDTO product)
        {
            return Ok(await _productService.UpdateProduct(productId, product));
        }

        /// <summary>
        /// Deletes a specific product.
        /// </summary>
        /// <param name="productId">The ID of the product to delete.</param>
        /// <returns>An action result representing the deletion operation.</returns>
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
