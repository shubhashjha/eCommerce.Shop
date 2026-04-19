using eCommerce.ShoppingCartService.Models.Dto;

namespace eCommerce.ShoppingCartService.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}

