using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Domain.Core.Bus
{
    // <summary>
    /// Represents an event handler interface for a specific event type.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to handle.</typeparam>
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent: Event
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="event">The event to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(TEvent @event);
    }

    /// <summary>
    /// Represents a base event handler interface.
    /// </summary>
    public interface IEventHandler { }
}
