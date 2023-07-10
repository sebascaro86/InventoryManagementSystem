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


        public Task Handle(UpdatedProductsEvent @event)
        {
            Console.WriteLine(@event.Products.FirstOrDefault().ProductId);
            return Task.CompletedTask;
        }
    }
}
