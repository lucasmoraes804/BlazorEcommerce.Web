using BlazorEcommerce.Api.Entities;
using BlazorEcommerce.Models.DTOs;

namespace BlazorEcommerce.Api.Mappings;

public static class MappingDtos
{
    public static IEnumerable<CategoryDto> ConvertCategoriesByDto(
                                        this IEnumerable<Category> categories)
    {
        return (from category in categories
                select new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    IconCSS = category.IconCSS
                }).ToList();
    }
    public static IEnumerable<ProductDto> ConvertProductsByDto(
                                         this IEnumerable<Product> products)
    {
        return (from product in products
                select new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Amount = product.Amount,
                    CategoryId = product.Category.Id,
                    CategoryName = product.Category.Name
                }).ToList();
    }
    public static ProductDto ConvertProductsByDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Amount = product.Amount,
            CategoryId = product.Category.Id,
            CategoryName = product.Category.Name
        };
    }

    public static IEnumerable<CartItemDto> ConvertCartItemsByDto(
        this IEnumerable<CartItem> carrinhoItens, IEnumerable<Product> produtos)
    {
        return (from cartItem in carrinhoItens
                join produto in produtos
                on cartItem.ProductId equals produto.Id
                select new CartItemDto
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = produto.Name,
                    ProductDescription = produto.Description,
                    ProductImageURL = produto.ImageUrl,
                    Price = produto.Price,
                    CartId = cartItem.CartId,
                    Amount = cartItem.Amount,
                    PriceTotal = produto.Price * cartItem.Amount,
                }).ToList();
    }

    public static CartItemDto ConvertCartItemByDto(this CartItem carrinhoItem,
                                               Product produto)
    {
        return new CartItemDto
        {
            Id = carrinhoItem.Id,
            ProductId = carrinhoItem.ProductId,
            ProductName = produto.Name,
            ProductDescription = produto.Description,
            ProductImageURL = produto.ImageUrl,
            Price = produto.Price,
            CartId = carrinhoItem.CartId,
            Amount = carrinhoItem.Amount,
            PriceTotal = produto.Price * carrinhoItem.Amount
        };
    }
}
