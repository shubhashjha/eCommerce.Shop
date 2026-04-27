using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseServices baseService;

        public AuthService(IBaseServices baseService)
        {
            this.baseService = baseService;
        }

        public Task<ResponseDTO> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = registrationRequestDto,
                URL = SystemEnums.AUTH_BASE_URL + "/api/auth/AssignRole"
            });
        }

        public Task<ResponseDTO> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = loginRequestDto,
                URL = SystemEnums.AUTH_BASE_URL + "/api/auth/login"
            }, withBearer: false);
        }

        public Task<ResponseDTO> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = registrationRequestDto,
                URL = SystemEnums.AUTH_BASE_URL + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
