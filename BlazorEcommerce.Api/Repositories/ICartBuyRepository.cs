using BlazorEcommerce.Api.Entities;
using BlazorEcommerce.Models.DTOs;

namespace BlazorEcommerce.Api.Repositories;

public interface ICartBuyRepository
{
    Task<CartItem> AddItem(CartItemAddsDto cartItemAddsDto);

    Task<CartItem> UpdateAmount(int id, CartItemUpdateAmountDto
        cartItemUpdateAmountDto);

    Task<CartItem> DeleteItem(int id);

    Task<CartItem> GetItem(int id);

    Task<IEnumerable<CartItem>> GetItems(string userId);
}
