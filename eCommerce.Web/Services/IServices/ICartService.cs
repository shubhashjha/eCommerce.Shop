using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface ICartService
    {
        Task<ResponseDTO> GetCartByUserIdAsync(string userId);
        Task<ResponseDTO> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDTO> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDTO> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDTO> EmailCartAsync(CartDto cartDto);
    }
}
