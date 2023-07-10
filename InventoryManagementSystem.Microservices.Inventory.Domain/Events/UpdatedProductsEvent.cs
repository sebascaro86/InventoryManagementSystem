using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Microservices.Inventory.Domain.Events
{
    public class UpdatedProductsEvent : Event
    {
        public ICollection<UpdateProductCommand> Products { get; protected set; }

        public UpdatedProductsEvent(ICollection<UpdateProductCommand> products)
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
