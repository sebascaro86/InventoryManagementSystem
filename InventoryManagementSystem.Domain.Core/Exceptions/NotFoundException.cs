namespace InventoryManagementSystem.Domain.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a resource is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
