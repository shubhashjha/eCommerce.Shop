using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseServices baseService;

        public CartService(IBaseServices baseService)
        {
            this.baseService = baseService;
        }

        public Task<ResponseDTO> ApplyCouponAsync(CartDto cartDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = cartDto,
                URL = SystemEnums.SHOPPING_CART_BASE_URL + "/api/cart/ApplyCoupon"
            });
        }

        public Task<ResponseDTO> EmailCartAsync(CartDto cartDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = cartDto,
                URL = SystemEnums.SHOPPING_CART_BASE_URL + "/api/cart/EmailCartRequest"
            });
        }

        public Task<ResponseDTO> GetCartByUserIdAsync(string userId)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.GET,
                URL = SystemEnums.SHOPPING_CART_BASE_URL + "/api/cart/GetCart/" + userId
            });
        }

        public Task<ResponseDTO> RemoveFromCartAsync(int cartDetailsId)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = cartDetailsId,
                URL = SystemEnums.SHOPPING_CART_BASE_URL + "/api/cart/RemoveCart"
            });
        }

        public Task<ResponseDTO> UpsertCartAsync(CartDto cartDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = cartDto,
                URL = SystemEnums.SHOPPING_CART_BASE_URL + "/api/cart/CartUpsert"
            });
        }
    }
}
