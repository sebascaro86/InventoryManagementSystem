using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.Core.Models
{
    /// <summary>
    /// Represents a client entity.
    /// </summary>
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IdType { get; set; }
        public int Identification { get; set; }

        public virtual ICollection<Buy> Buys { get; set; }
    }
}
