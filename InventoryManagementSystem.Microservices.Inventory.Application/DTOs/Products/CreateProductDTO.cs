namespace InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public int InInventory { get; set; }
        public bool Enabled { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
