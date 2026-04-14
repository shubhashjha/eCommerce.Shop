using eCommerce.AuthService.Models;

namespace eCommerce.AuthService.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
