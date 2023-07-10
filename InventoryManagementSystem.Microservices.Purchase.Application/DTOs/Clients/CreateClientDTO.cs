namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients
{
    /// <summary>
    /// Represents a create client data transfer object.
    /// </summary>
    public class CreateClientDTO
    {
        public string IdType { get; set; }
        public string Name { get; set; }
        public int Identification { get; set; }
    }
}
