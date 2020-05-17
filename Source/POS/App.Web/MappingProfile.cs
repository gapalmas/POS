using App.Core.Entities;
using App.Web.Mappers;
using AutoMapper;

namespace App.Web
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, InventoryDTO>();
            CreateMap<Product, AddInventoryDTO>();
            CreateMap<Product, RemoveInventoryDTO>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<Purchaseorder, PurchaseDTO>();
        }
    }
}