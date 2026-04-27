using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDTO> CreateOrderAsync(CartDto cartDto);
        Task<ResponseDTO> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto);
        Task<ResponseDTO> GetAllOrderAsync(string? userId);
        Task<ResponseDTO> GetOrderAsync(int orderId);
        Task<ResponseDTO> UpdateOrderStatusAsync(int orderId, string newStatus);
        Task<ResponseDTO> ValidateStripeSessionAsync(int orderHeaderId);
    }
}
