using BlazorEcommerce.Api.Context;
using BlazorEcommerce.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetItem(int id)
        {
            var produto = await _context.Products
                          .Include(c => c.Category)
                          .SingleOrDefaultAsync(c => c.Id == id);

            return produto;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var produtos = await _context.Products
                                  .Include(p => p.Category)
            .ToListAsync();
            return produtos;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var produtos = await _context.Products
                                .Include(p => p.Category)
                                .Where(p => p.CategoryId == id).ToListAsync();
            return produtos;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categorias = await _context.Categories.ToListAsync();
            return categorias;
        }
    }
}
