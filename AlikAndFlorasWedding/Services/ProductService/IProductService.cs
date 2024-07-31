using AlikAndFlorasWedding.Models.Dtos;
using AlikAndFlorasWedding.Models;

namespace AlikAndFlorasWedding.Services.ProductService;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(int? categoryId = null);
    Task<bool> AddProductAsync(ProductDto product);
    Task<string> SaveProductImageAsync(IFormFile file);
    Task<bool> DeleteProductAsync(int id);

    Task<ProductDto?> GetProductAsync(int id);
    Task<ProductDto?> UpdateProductAsync(ProductDto product);
}