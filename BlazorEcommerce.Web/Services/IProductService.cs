using BlazorEcommerce.Models.DTOs;

namespace BlazorEcommerce.Web.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetItems();
    Task<ProductDto> GetItem(int id);

    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
}
