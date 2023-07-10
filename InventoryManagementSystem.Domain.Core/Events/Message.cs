using MediatR;

namespace InventoryManagementSystem.Domain.Core.Events
{
    /// <summary>
    /// Represents an abstract base class for messages.
    /// </summary>
    public abstract class Message : IRequest<bool>
    {
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        public string MessageType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
