namespace InventoryManagementSystem.Microservices.Inventory.API.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
