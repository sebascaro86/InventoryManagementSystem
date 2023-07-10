using InventoryManagementSystem.Domain.Core.Commands;
using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Domain.Core.Bus
{
    /// <summary>
    /// Defines the interface for an event bus.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Sends a command to be processed.
        /// </summary>
        /// <typeparam name="T">The type of command.</typeparam>
        /// <param name="command">The command to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendCommand<T>(T command) where T : Command;

        /// <summary>
        /// Publishes an event to be handled by event handlers.
        /// </summary>
        /// <typeparam name="T">The type of event.</typeparam>
        /// <param name="event">The event to publish.</param>
        void Publish<T>(T @event) where T : Event;

        /// <summary>
        /// Subscribes an event handler to handle events of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of event.</typeparam>
        /// <typeparam name="TH">The type of event handler.</typeparam>
        void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>;
    }
}
