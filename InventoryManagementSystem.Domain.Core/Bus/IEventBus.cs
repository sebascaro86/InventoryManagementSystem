using InventoryManagementSystem.Domain.Core.Commands;
using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>;
    }
}
