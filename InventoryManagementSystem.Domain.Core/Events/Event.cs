namespace InventoryManagementSystem.Domain.Core.Events
{
    /// <summary>
    /// Represents an abstract base class for events.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Gets or sets the timestamp of the event.
        /// </summary>
        public DateTime Tiemestamp { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        protected Event()
        {
            Tiemestamp = DateTime.Now;
        }
    }
}
