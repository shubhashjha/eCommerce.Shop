using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class CouponService: ICouponService
    {
        private readonly IBaseServices _baseService;
        public CouponService(IBaseServices baseService) 
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO> CreateCouponAsync(CouponDTO coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = Utility.SystemEnums.APITYPE.POST,
                URL = SystemEnums.COUPON_BASE_URL + $"/api/coupon",
                RequestBody = coupon
            });
        }

        public async Task<ResponseDTO> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = Utility.SystemEnums.APITYPE.DELETE,
                URL = SystemEnums.COUPON_BASE_URL + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponseDTO> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new RequestDto() 
            {
                APIType = Utility.SystemEnums.APITYPE.GET,
                URL = SystemEnums.COUPON_BASE_URL + "/api/coupon"
            });
        }

        public async Task<ResponseDTO> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = Utility.SystemEnums.APITYPE.GET,
                URL = SystemEnums.COUPON_BASE_URL + $"/api/coupon/GetByCode/{couponCode}"
            });
        }

        public async Task<ResponseDTO> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = Utility.SystemEnums.APITYPE.GET,
                URL = SystemEnums.COUPON_BASE_URL + $"/api/coupon/{id}"
            });
        }

        public async Task<ResponseDTO> UpdateCouponAsync(CouponDTO coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                APIType = Utility.SystemEnums.APITYPE.PUT,
                URL = SystemEnums.COUPON_BASE_URL + $"/api/coupon",
                RequestBody = coupon
            });
        }
    }
}
