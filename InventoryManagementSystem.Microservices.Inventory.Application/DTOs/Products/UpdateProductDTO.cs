namespace InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products
{
    /// <summary>
    /// Represents the data transfer object for updating a product.
    /// </summary>
    public class UpdateProductDTO
    {
        public string Name { get; set; }
        public int InInventory { get; set; }
        public bool Enabled { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
