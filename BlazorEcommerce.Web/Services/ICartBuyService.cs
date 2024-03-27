using BlazorEcommerce.Models.DTOs;

namespace BlazorEcommerce.Web.Services;

public interface ICartBuyService
{
    Task<CartItemDto> AddItem(CartItemAddsDto cartItemAddsDto);

    Task<CartItemDto> UpdateAmount(CartItemUpdateAmountDto cartItemUpdateAmountDto);

    Task<CartItemDto> DeleteItem(int id);

    Task<List<CartItemDto>> GetItems(string userId);

}
