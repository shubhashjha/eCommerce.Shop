using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<ResponseDTO> GetCouponAsync(string couponCode);
        Task<ResponseDTO> GetAllCouponAsync();
        Task<ResponseDTO> GetCouponByIdAsync(int id);
        Task<ResponseDTO> CreateCouponAsync(CouponDTO coupon);
        Task<ResponseDTO> UpdateCouponAsync(CouponDTO coupon);
        Task<ResponseDTO> DeleteCouponAsync(int id);
    }
}
