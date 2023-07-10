using InventoryManagementSystem.Domain.Core.Events;
using InventoryManagementSystem.Microservices.Purchase.Domain.Commands;

namespace InventoryManagementSystem.Microservices.Purchase.Domain.Events
{
    public class UpdatedProductsEvent : Event
    {
        public ICollection<UpdateProductCommand> Products { get; protected set; }

        public UpdatedProductsEvent(ICollection<UpdateProductCommand> products)
        {
            Products = products;
        }
    }
}
