using InventoryManagementSystem.Domain.Core.Bus;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;
using InventoryManagementSystem.Microservices.Purchase.Domain.Events;
using MediatR;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.CommandHandlers
{
    /// <summary>
    /// Represents a command handler for updating products.
    /// </summary>
    public class UpdateProductsCommandHandler : IRequestHandler<UpdateProductsCommand, bool>
    {
        private readonly IEventBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductsCommandHandler"/> class.
        /// </summary>
        /// <param name="bus">The event bus.</param>
        public UpdateProductsCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Handles the update products command by publishing an updated products event.
        /// </summary>
        /// <param name="request">The update products command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<bool> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new UpdatedProductsEvent(request.Products.ToList()));
            return Task.FromResult(true);
        }
    }
}
