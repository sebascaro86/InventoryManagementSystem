namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string IdType { get; set; }
        public string Name { get; set; }
        public int Identification { get; set; }
    }
}
