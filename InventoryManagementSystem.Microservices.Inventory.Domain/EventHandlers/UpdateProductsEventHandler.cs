using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Microservices.Inventory.Domain.Events;
using InventoryManagementSystem.Microservices.Inventory.Domain.Interfaces;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.EventHandlers
{
    /// <summary>
    /// Represents the event handler for UpdatedProductsEvent.
    /// </summary>
    public class UpdateProductsEventHandler : IEventHandler<UpdatedProductsEvent>
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductsEventHandler"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public UpdateProductsEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the UpdatedProductsEvent.
        /// </summary>
        /// <param name="event">The UpdatedProductsEvent to handle.</param>
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
