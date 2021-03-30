using AutoMapper;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Core.Entites;

namespace SmileBoyClient
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.UpdateBy, opt => opt.Ignore());

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>()
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.UpdateBy, opt => opt.Ignore());
        }
    }
}
