using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface IBaseServices
    {
        Task<ResponseDTO> SendAsync(RequestDto request);
    }
}
