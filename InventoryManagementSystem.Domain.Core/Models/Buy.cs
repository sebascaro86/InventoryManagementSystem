using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.Core.Models
{
    public class Buy
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Client")]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<BuyItem> Products { get; set; }
    }
}
