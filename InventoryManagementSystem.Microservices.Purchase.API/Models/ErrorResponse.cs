namespace InventoryManagementSystem.Microservices.Purchase.API.Models
{
    /// <summary>
    /// Represents an error response.
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
