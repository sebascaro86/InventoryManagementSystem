namespace InventoryManagementSystem.Infrastructure.Bus
{
    /// <summary>
    /// Represents the RabbitMQ settings.
    /// </summary>
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
