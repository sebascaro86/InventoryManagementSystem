using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;
using InventoryManagementSystem.Microservices.Purchase.Domain.Events;
using MediatR;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.CommandHandlers
{
    public class UpdateProductsCommandHandler : IRequestHandler<UpdateProductsCommand, bool>
    {
        private readonly IEventBus _bus;

        public UpdateProductsCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new UpdatedProductsEvent(request.Products.ToList()));
            return Task.FromResult(true);
        }
    }
}
