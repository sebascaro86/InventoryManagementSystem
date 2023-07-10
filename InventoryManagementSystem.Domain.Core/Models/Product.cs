using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.Core.Models
{
    /// <summary>
    /// Represents a product entity.
    /// </summary>
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int InInventory { get; set; }
        public bool Enabled { get; set; }
        public Double Min { get; set; }
        public Double Max { get; set; }
    }
}
