using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;
using InventoryManagementSystem.Microservices.Inventory.Application.Interfaces;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Inventory.Application.Services
{
    /// <summary>
    /// Represents the implementation of the product service.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="mapper">The mapper.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            List<Product> products = (await _productRepository.GetAll()).ToList();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        /// <inheritdoc/>
        public async Task<ProductDTO> GetProduct(string productId)
        {
            Product product = await _productRepository.Get(Guid.Parse(productId));
            return _mapper.Map<ProductDTO>(product);
        }

        /// <inheritdoc/>
        public async Task<ProductDTO> CreateProduct(CreateProductDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            await _productRepository.Create(product);
            return _mapper.Map<ProductDTO>(product);
        }

        /// <inheritdoc/>
        public async Task<ProductDTO> UpdateProduct(string productId, UpdateProductDTO productDTO)
        {
            Product product = await _productRepository.Get(Guid.Parse(productId));

            product.Name = productDTO.Name;
            product.InInventory = productDTO.InInventory;
            product.Enabled = productDTO.Enabled;
            product.Max = productDTO.Max;
            product.Min = productDTO.Min;

            product = await _productRepository.Update(product);
            return _mapper.Map<ProductDTO>(product);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProduct(string productId)
        {
            return await _productRepository.Delete(Guid.Parse(productId));
        }
    }
}
