using AutoMapper;
using eCommerce.ProductService.Models;
using eCommerce.ProductService.Models.Dto;

namespace eCommerce.ProductService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}

