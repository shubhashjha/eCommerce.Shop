using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseServices baseService;

        public OrderService(IBaseServices baseService)
        {
            this.baseService = baseService;
        }

        public Task<ResponseDTO> CreateOrderAsync(CartDto cartDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = cartDto,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/CreateOrder"
            });
        }

        public Task<ResponseDTO> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = stripeRequestDto,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/CreateStripeSession"
            });
        }

        public Task<ResponseDTO> GetAllOrderAsync(string? userId)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.GET,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/GetOrders?userId=" + userId
            });
        }

        public Task<ResponseDTO> GetOrderAsync(int orderId)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.GET,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/GetOrder/" + orderId
            });
        }

        public Task<ResponseDTO> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = newStatus,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/UpdateOrderStatus/" + orderId
            });
        }

        public Task<ResponseDTO> ValidateStripeSessionAsync(int orderHeaderId)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = orderHeaderId,
                URL = SystemEnums.ORDER_BASE_URL + "/api/order/ValidateStripeSession"
            });
        }
    }
}
