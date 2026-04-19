using eCommerce.ShoppingCartService.Models.Dto;

namespace eCommerce.ShoppingCartService.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}

