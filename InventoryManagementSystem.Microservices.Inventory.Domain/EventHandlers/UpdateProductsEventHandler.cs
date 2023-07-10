using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Microservices.Inventory.Domain.Events;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.EventHandlers
{
    public class UpdateProductsEventHandler : IEventHandler<UpdatedProductsEvent>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductsEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task Handle(UpdatedProductsEvent @event)
        {
            foreach (var updateProductCommand in @event.Products)
            {
                var product = await _productRepository.Get(updateProductCommand.ProductId);

                if (product != null)
                {
                    product.InInventory -= updateProductCommand.Quantity;
                    await _productRepository.Update(product);
                }
            }
        }
    }
}
