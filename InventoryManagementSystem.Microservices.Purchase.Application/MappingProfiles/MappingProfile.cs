using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Clients;
using InventoryManagementSystem.Microservices.Purchase.Application.DTOs.Purchases;

namespace InventoryManagementSystem.Microservices.Purchase.Application.MappingProfiles
{
    /// <summary>
    /// Represents a mapping profile for AutoMapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            CreateMap<CreateClientDTO, Client>();

            CreateMap<ProductPurchaseDTO, Product>();
            CreateMap<Product, ProductPurchaseDTO>();

            CreateMap<RegisterPurchaseDTO, Buy>();
            CreateMap<ProductPurchaseDTO, BuyItem>();
            CreateMap<BuyItem, ProductPurchaseDTO>();

            CreateMap<PurchaseDTO, Buy>();
            CreateMap<Buy, PurchaseDTO>();
        }
    }
}
