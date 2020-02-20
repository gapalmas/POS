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
        }
    }
}