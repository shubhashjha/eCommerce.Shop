using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace eCommerce.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize]
        public IActionResult OrderIndex()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new();
            string? userId = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var response = await orderService.GetOrderAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result)) ?? new OrderHeaderDto();
            }

            if (!User.IsInRole(SystemEnums.ROLE_ADMIN) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }

            return View(orderHeaderDto);
        }

        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await orderService.UpdateOrderStatusAsync(orderId, SystemEnums.STATUS_READY_FOR_PICKUP);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId });
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await orderService.UpdateOrderStatusAsync(orderId, SystemEnums.STATUS_COMPLETED);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId });
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await orderService.UpdateOrderStatusAsync(orderId, SystemEnums.STATUS_CANCELLED);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId });
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = string.Empty;
            if (!User.IsInRole(SystemEnums.ROLE_ADMIN))
            {
                userId = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value ?? string.Empty;
            }

            ResponseDTO response = orderService.GetAllOrderAsync(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result)) ?? new List<OrderHeaderDto>();
                switch (status)
                {
                    case "approved":
                        list = list.Where(u => u.Status == SystemEnums.STATUS_APPROVED);
                        break;
                    case "readyforpickup":
                        list = list.Where(u => u.Status == SystemEnums.STATUS_READY_FOR_PICKUP);
                        break;
                    case "cancelled":
                        list = list.Where(u => u.Status == SystemEnums.STATUS_CANCELLED || u.Status == SystemEnums.STATUS_REFUNDED);
                        break;
                }
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }

            return Json(new { data = list.OrderByDescending(u => u.OrderHeaderId) });
        }
    }
}
