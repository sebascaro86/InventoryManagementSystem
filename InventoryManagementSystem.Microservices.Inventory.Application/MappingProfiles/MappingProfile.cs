using AutoMapper;
using InventoryManagementSystem.Domain.Core.Models;
using InventoryManagementSystem.Microservices.Inventory.Application.DTOs.Products;

namespace InventoryManagementSystem.Microservices.Inventory.Application.MappingProfiles
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
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<CreateProductDTO, Product>();

            /*CreateMap<BuildingProperty, BuildingPropertyDTO>();
            CreateMap<BuildingPropertyDTO, BuildingProperty>();
            CreateMap<BuildingPropertyImage, BuildingPropertyImageDTO>();

            CreateMap<CreateBuildingPropertyDTO, BuildingProperty>();
            CreateMap<UpdateBuildingPropertyDTO, BuildingProperty>();

            CreateMap<BuildingPropertyFilterDTO, BuildingPropertyFilter>();*/
        }
    }
}
