﻿using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Infrastructure.Database.Models
{
    public class BuyItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Buy")]
        public Guid BuyId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Buy Buy { get; set; }

    }
}
