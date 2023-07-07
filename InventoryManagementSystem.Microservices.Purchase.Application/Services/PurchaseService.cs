using AutoMapper;
using InventoryManagementSystem.Domain.Core.Exceptions;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;
using InventoryManagementSystem.Microservices.Purchase.Application.Interfaces;
using InventoryManagementSystem.Microservices.Purchase.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Microservices.Purchase.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, 
            IProductRepository productRepository, 
            IClientRepository clientRepository, 
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PurchaseDTO>> GetPurchases()
        {
            List<Buy> purchases = (await _purchaseRepository.GetBuys()).ToList();
            return _mapper.Map<List<PurchaseDTO>>(purchases);
        }

        public async Task<PurchaseDTO> GetPurchase(string purchaseId)
        {
            Buy purchase = await _purchaseRepository.GetBuy(Guid.Parse(purchaseId));
            return _mapper.Map<PurchaseDTO>(purchase);
        }

        public async Task<PurchaseDTO> RegisterPurchase(RegisterPurchaseDTO purchaseDTO)
        {
            await _clientRepository.GetClient(Guid.Parse(purchaseDTO.CustomerId));

            var productIds = purchaseDTO.Products.Select(product => product.Id).ToList();
            var products = await _productRepository.GetProductsByIds(productIds);

            foreach (var product in purchaseDTO.Products)
            {
                var productEntity = products.FirstOrDefault(p => p.Id == product.Id);
                if (productEntity == null)
                    throw new NotFoundException($"Product not found: {product.Id}");

                if (product.Quantity > productEntity.InInventory)
                    throw new ValidationException($"There is not enough inventory for the product: {product.Id}");

                if (product.Quantity < productEntity.Min || product.Quantity > productEntity.Max)
                    throw new ValidationException($"The quantity of the product {product.Id} does not meet established limits");
            }


            Buy purchase = _mapper.Map<Buy>(purchaseDTO);
            //var registeredPurchase = await _purchaseRepository.Re(purchase);
            return null;
        }
    }
}
