namespace InventoryManagementSystem.Microservices.Purchase.API.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
