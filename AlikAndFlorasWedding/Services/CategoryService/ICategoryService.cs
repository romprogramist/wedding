using AlikAndFlorasWedding.Models.Dtos;

namespace AlikAndFlorasWedding.Services.CategoryService;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
}