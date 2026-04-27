using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using eCommerce.Web.Utility;

namespace eCommerce.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseServices baseService;

        public ProductService(IBaseServices baseService)
        {
            this.baseService = baseService;
        }

        public Task<ResponseDTO> CreateProductAsync(ProductDto productDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.POST,
                RequestBody = productDto,
                URL = SystemEnums.PRODUCT_BASE_URL + "/api/product",
                ContentType = SystemEnums.CONTENTTYPE.MultipartFormData
            });
        }

        public Task<ResponseDTO> DeleteProductAsync(int id)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.DELETE,
                URL = SystemEnums.PRODUCT_BASE_URL + "/api/product/" + id
            });
        }

        public Task<ResponseDTO> GetAllProductsAsync()
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.GET,
                URL = SystemEnums.PRODUCT_BASE_URL + "/api/product"
            });
        }

        public Task<ResponseDTO> GetProductByIdAsync(int id)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.GET,
                URL = SystemEnums.PRODUCT_BASE_URL + "/api/product/" + id
            });
        }

        public Task<ResponseDTO> UpdateProductAsync(ProductDto productDto)
        {
            return baseService.SendAsync(new RequestDto
            {
                APIType = SystemEnums.APITYPE.PUT,
                RequestBody = productDto,
                URL = SystemEnums.PRODUCT_BASE_URL + "/api/product",
                ContentType = SystemEnums.CONTENTTYPE.MultipartFormData
            });
        }
    }
}
