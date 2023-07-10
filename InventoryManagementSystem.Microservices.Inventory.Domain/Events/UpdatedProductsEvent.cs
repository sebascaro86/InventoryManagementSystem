using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.Events
{
    /// <summary>
    /// Represents the event for updated products.
    /// </summary>
    public class UpdatedProductsEvent : Event
    {
        /// <summary>
        /// Gets the collection of update product commands.
        /// </summary>
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

    /// <summary>
    /// Represents the command to update a product.
    /// </summary>
    public class UpdateProductCommand
    {
        public Guid ProductId { get; protected set; }
        public int Quantity { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommand"/> class.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="quantity">The quantity to update for the product.</param>
        public UpdateProductCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
