namespace InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int InInventory { get; set; }
        public bool Enabled { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
