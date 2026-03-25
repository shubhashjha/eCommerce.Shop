using AutoMapper;
using eCommerce.CouponService.Model;
using eCommerce.CouponService.Model.DTO;

namespace eCommerce.CouponService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegMaps() 
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.CreateMap<CouponDTO,Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            }); 
            return mappingConfig;
        }
    }
}
