using InventoryManagementSystem.Domain.Core.Commands;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Commands
{
    public class UpdateProductsCommand : Command
    {
        public ICollection<UpdateProductCommand> Products { get; protected set; }

        public UpdateProductsCommand(ICollection<UpdateProductCommand> products)
        {
            Products = products;
        }
    }

    public class UpdateProductCommand
    {
        public Guid ProductId { get; protected set; }
        public int Quantity { get; protected set; }

        public UpdateProductCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
