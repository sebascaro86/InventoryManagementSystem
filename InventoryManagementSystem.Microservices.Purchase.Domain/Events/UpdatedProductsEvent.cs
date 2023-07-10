using InventoryManagementSystem.Domain.Core.Events;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Events
{
    /// <summary>
    /// Represents an event for the updated products.
    /// </summary>
    public class UpdatedProductsEvent : Event
    {
        public ICollection<UpdateProductCommand> Products { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedProductsEvent"/> class.
        /// </summary>
        /// <param name="products">The collection of update product commands.</param>
        public UpdatedProductsEvent(ICollection<UpdateProductCommand> products)
        {
            Products = products;
        }
    }
}
