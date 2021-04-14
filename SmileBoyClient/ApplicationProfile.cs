using AutoMapper;
using SmileBoy.Client.Core.Dto;
using SmileBoy.Client.Entities;
using SmileBoy.Client.Entities.Entities;
using SmileBoyClient.Core.Entites;
using System.Linq;

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
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.UpdateBy, opt => opt.Ignore());

            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Customer, opt => opt.Ignore())
                .ForMember(o => o.Products, opt => opt.Ignore());

            CreateMap<OrderDto, Order>()
               .ForMember(o => o.CreatedBy, opt => opt.Ignore())
               .ForMember(o => o.UpdateBy, opt => opt.Ignore())
               .ForMember(o => o.ProductsIds, opt => opt.Ignore())
               .ForMember(o => o.CustomerId, opt => opt.Ignore());

            CreateMap<OrderUpdate, Order>()
               .ForMember(o => o.CreatedBy, opt => opt.Ignore())
               .ForMember(o => o.UpdateBy, opt => opt.Ignore());

            CreateMap<OrderDto, OrderUpdate>()
               .ForMember(o => o.CustomerId, opt => opt.MapFrom(o => o.Customer.Id))
               .ForMember(o => o.ProductsIds, opt => opt.MapFrom(o => o.Products.Select(p => p.Id)));
        }
    }
}
