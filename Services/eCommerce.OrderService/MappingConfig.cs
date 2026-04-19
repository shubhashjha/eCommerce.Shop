using AutoMapper;
using eCommerce.OrderService.Model;
using eCommerce.OrderService.Model.Dto;

namespace eCommerce.OrderService
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal))
                .ReverseMap();

            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product != null ? src.Product.Price : 0));

            CreateMap<OrderDetailsDto, CartDetailsDto>();
            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
            CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
        }
    }
}
