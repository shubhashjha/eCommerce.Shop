using AutoMapper;
using eCommerce.CouponService.Model;
using eCommerce.CouponService.Model.DTO;

namespace eCommerce.CouponService
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CouponDTO, Coupon>();
            CreateMap<Coupon, CouponDTO>();
        }
    }
}
