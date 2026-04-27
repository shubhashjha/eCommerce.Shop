using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace eCommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            this.cartService = cartService;
            this.orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            if (cart.CartHeader == null || cartDto.CartHeader == null)
            {
                TempData["error"] = "Cart is empty.";
                return View(cart);
            }

            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;

            var response = await orderService.CreateOrderAsync(cart);
            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto? orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if (orderHeaderDto is null)
                {
                    TempData["error"] = "Unable to create order.";
                    return View(cart);
                }

                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto
                };

                var stripeResponse = await orderService.CreateStripeSessionAsync(stripeRequestDto);
                StripeRequestDto? stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));
                if (stripeResponseResult?.StripeSessionUrl is not null)
                {
                    Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);
                    return new StatusCodeResult(303);
                }
            }

            TempData["error"] = response?.Message;
            return View(cart);
        }

        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDTO? response = await orderService.ValidateStripeSessionAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto? orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
                if (orderHeader?.Status == SystemEnums.STATUS_APPROVED)
                {
                    return View(orderId);
                }
            }

            return View(orderId);
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            ResponseDTO? response = await cartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDTO? response = await cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            if (cart.CartHeader != null)
            {
                cart.CartHeader.Email = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value;
            }

            ResponseDTO? response = await cartService.EmailCartAsync(cart);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }

            TempData["error"] = response?.Message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            if (cartDto.CartHeader != null)
            {
                cartDto.CartHeader.CouponCode = string.Empty;
            }

            ResponseDTO? response = await cartService.ApplyCouponAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }

            TempData["error"] = response?.Message;
            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            ResponseDTO? response = await cartService.GetCartByUserIdAsync(userId ?? string.Empty);
            if (response != null && response.IsSuccess)
            {
                CartDto? cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto ?? new CartDto();
            }

            return new CartDto();
        }
    }
}
