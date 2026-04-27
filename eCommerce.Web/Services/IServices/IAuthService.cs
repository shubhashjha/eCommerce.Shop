using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDTO> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDTO> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDTO> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}
