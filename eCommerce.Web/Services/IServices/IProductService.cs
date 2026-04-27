using eCommerce.Web.Models;

namespace eCommerce.Web.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDTO> GetAllProductsAsync();
        Task<ResponseDTO> GetProductByIdAsync(int id);
        Task<ResponseDTO> CreateProductAsync(ProductDto productDto);
        Task<ResponseDTO> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDTO> DeleteProductAsync(int id);
    }
}
