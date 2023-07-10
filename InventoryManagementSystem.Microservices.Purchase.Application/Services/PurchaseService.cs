using AutoMapper;
using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Services
{
    /// <summary>
    /// Represents the purchase service implementation.
    /// </summary>
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEventBus _bus;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseService"/> class.
        /// </summary>
        /// <param name="purchaseRepository">The purchase repository.</param>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="clientRepository">The client repository.</param>
        /// <param name="bus">The event bus.</param>
        /// <param name="mapper">The mapper.</param>
        public PurchaseService(
            IPurchaseRepository purchaseRepository, 
            IProductRepository productRepository, 
            IClientRepository clientRepository,
            IEventBus bus,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _bus = bus;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<ICollection<PurchaseDTO>> GetPurchases()
        {
            List<Buy> purchases = (await _purchaseRepository.GetBuys()).ToList();
            return _mapper.Map<List<PurchaseDTO>>(purchases);
        }

        /// <inheritdoc />
        public async Task<PurchaseDTO> GetPurchase(string purchaseId)
        {
            Buy purchase = await _purchaseRepository.GetBuy(Guid.Parse(purchaseId));
            return _mapper.Map<PurchaseDTO>(purchase);
        }

        /// <inheritdoc />
        public async Task<PurchaseDTO> RegisterPurchase(RegisterPurchaseDTO purchaseDTO)
        {
            var client = await _clientRepository.GetClient(Guid.Parse(purchaseDTO.ClientId));

            var productIds = purchaseDTO.Products.Select(product => Guid.Parse(product.ProductId)).ToList();
            var products = await _productRepository.GetProductsByIds(productIds);

            foreach (var product in purchaseDTO.Products)
            {
                var productEntity = products.FirstOrDefault(p => p.Id == Guid.Parse(product.ProductId));
                if (productEntity == null)
                    throw new NotFoundException($"Product not found: {product.ProductId}");

                if (product.Quantity > productEntity.InInventory)
                    throw new BadRequestException($"There is not enough inventory for the product: {product.ProductId}");

                if (product.Quantity < productEntity.Min || product.Quantity > productEntity.Max)
                    throw new BadRequestException($"The quantity of the product {product.ProductId} does not meet established limits");
            }

            Buy purchase = _mapper.Map<Buy>(purchaseDTO);
            var registeredPurchase = await _purchaseRepository.CreateBuy(purchase);

            UpdatedProductQuantity(registeredPurchase);
            
            registeredPurchase.Client = client;
            return _mapper.Map<PurchaseDTO>(registeredPurchase);
        }

        /// <summary>
        /// Updates the quantity of products in the inventory by sending update commands.
        /// </summary>
        /// <param name="buy">The buy object containing the purchased products.</param>
        private void UpdatedProductQuantity(Buy buy)
        {
            var products = buy.Products.Select(p => new UpdateProductCommand(p.ProductId, p.Quantity));
            var updateProductCommand = new UpdateProductsCommand(products.ToList());

            _bus.SendCommand(updateProductCommand);
        }
    }
}
