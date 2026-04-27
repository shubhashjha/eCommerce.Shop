using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerce.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokenProvider tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            this.authService = authService;
            this.tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDTO responseDto = await authService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto? loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                if (loginResponseDto is null)
                {
                    TempData["error"] = "Unable to process login response.";
                    return View(obj);
                }

                await SignInUser(loginResponseDto);
                tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = responseDto.Message;
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.RoleList = BuildRoleList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDTO result = await authService.RegisterAsync(obj);

            if (result != null && result.IsSuccess)
            {
                obj.Role ??= SystemEnums.ROLE_CUSTOMER;
                ResponseDTO assignRole = await authService.AssignRoleAsync(obj);
                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            ViewBag.RoleList = BuildRoleList();
            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private static List<SelectListItem> BuildRoleList()
        {
            return new List<SelectListItem>
            {
                new() { Text = SystemEnums.ROLE_ADMIN, Value = SystemEnums.ROLE_ADMIN },
                new() { Text = SystemEnums.ROLE_CUSTOMER, Value = SystemEnums.ROLE_CUSTOMER }
            };
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value ?? string.Empty));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value ?? string.Empty));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value ?? string.Empty));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value ?? string.Empty));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
