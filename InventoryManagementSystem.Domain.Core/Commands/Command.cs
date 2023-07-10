using InventoryManagementSystem.Domain.Core.Events;

namespace InventoryManagementSystem.Domain.Core.Commands
{
    /// <summary>
    /// Represents an abstract base class for commands.
    /// </summary>
    public abstract class Command : Message
    {
        /// <summary>
        /// Gets or sets the timestamp of the command.
        /// </summary>
        public DateTime Timestamp { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
