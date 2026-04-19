using eCommerce.OrderService.Model.Dto;

namespace eCommerce.OrderService.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
