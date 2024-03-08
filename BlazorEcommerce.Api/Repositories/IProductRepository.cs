using BlazorEcommerce.Api.Entities;

namespace BlazorEcommerce.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<Product> GetItem(int id);

        Task<IEnumerable<Product>> GetItemsByCategory(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
