using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            contextAccessor.HttpContext?.Response.Cookies.Delete(SystemEnums.TOKEN_COOKIE);
        }

        public string? GetToken()
        {
            string? token = null;
            bool hasToken = contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SystemEnums.TOKEN_COOKIE, out token) ?? false;
            return hasToken ? token : null;
        }

        public void SetToken(string token)
        {
            contextAccessor.HttpContext?.Response.Cookies.Append(SystemEnums.TOKEN_COOKIE, token);
        }
    }
}
