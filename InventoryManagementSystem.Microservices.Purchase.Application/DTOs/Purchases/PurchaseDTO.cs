﻿using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;

namespace InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases
{
    public class PurchaseDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<ProductPurchaseDTO> Products { get; set; }
        public ClientDTO Client { get; set; }
    }
}
