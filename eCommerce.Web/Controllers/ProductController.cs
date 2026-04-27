using eCommerce.Web.Models;
using eCommerce.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();
            ResponseDTO? response = await productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ResponseDTO? response = await productService.CreateProductAsync(model);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(ProductIndex));
            }

            TempData["error"] = response?.Message;
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDTO? response = await productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            TempData["error"] = response?.Message;
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDTO? response = await productService.DeleteProductAsync(productDto.ProductId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }

            TempData["error"] = response?.Message;
            return View(productDto);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDTO? response = await productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }

            TempData["error"] = response?.Message;
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            ResponseDTO? response = await productService.UpdateProductAsync(productDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(ProductIndex));
            }

            TempData["error"] = response?.Message;
            return View(productDto);
        }
    }
}
