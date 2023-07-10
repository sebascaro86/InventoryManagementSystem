using InventoryManagementSystem.Domain.Core.Commands;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Commands
{
    /// <summary>
    /// Represents a command for updating products.
    /// </summary>
    public class UpdateProductsCommand : Command
    {
        public ICollection<UpdateProductCommand> Products { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductsCommand"/> class.
        /// </summary>
        /// <param name="products">The collection of update product commands.</param>
        public UpdateProductsCommand(ICollection<UpdateProductCommand> products)
        {
            Products = products;
        }
    }

    /// <summary>
    /// Represents a command for updating a specific product.
    /// </summary>
    public class UpdateProductCommand
    {
        public Guid ProductId { get; protected set; }
        public int Quantity { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCommand"/> class.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="quantity">The updated quantity of the product.</param>
        public UpdateProductCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
