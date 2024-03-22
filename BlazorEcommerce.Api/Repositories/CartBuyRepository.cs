using BlazorEcommerce.Api.Context;
using BlazorEcommerce.Api.Entities;
using BlazorEcommerce.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Api.Repositories;

public class CartBuyRepository : ICartBuyRepository
{
    private readonly AppDbContext _context;

    public CartBuyRepository(AppDbContext context)
    {
        _context = context;
    }

    private async Task<bool> CartItemExistes(int cartId, int productId)
    {
        return await _context.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                          c.ProductId == productId);
    }

    public async Task<CartItem> AddItem(CartItemAddsDto carrinhoItemAdicionaDto)
    {
        if (await CartItemExistes(carrinhoItemAdicionaDto.CartId, 
            carrinhoItemAdicionaDto.ProductId) == false)
        {
            //verifica se o produto existe 
            //cria um novo item no carrinho
            var item = await(from produto in _context.Products
                             where produto.Id == carrinhoItemAdicionaDto.ProductId
                             select new CartItem
                             {
                                 CartId = carrinhoItemAdicionaDto.CartId,
                                 ProductId = produto.Id,
                                 Amount = carrinhoItemAdicionaDto.Amount
                             }).SingleOrDefaultAsync();

            //se o item existe então incluir o item no carrinho
            if (item is not null)
            {
                var resultado = await _context.CartItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return resultado.Entity;
            }
        }
        return null;
    }

    public async Task<CartItem> UpdateAmount(int id,
               CartItemUpdateAmountDto cartItemUpdateAmountDto)
    {
        var cartItem = await _context.CartItems.FindAsync(id);

        if (cartItem is not null)
        {
            cartItem.Amount = cartItemUpdateAmountDto.Amount;
            await _context.SaveChangesAsync();
            return cartItem;
        }
        return null;
    }

    public async Task<CartItem> DeleteItem(int id)
    {
        var item = await _context.CartItems.FindAsync(id);

        if (item is not null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return item;
    }

    public async Task<CartItem> GetItem(int id)
    {
        return await (from cart in _context.Carts
                      join cartItem in _context.CartItems
                      on cart.Id equals cartItem.CartId
                      where cartItem.Id == id
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          ProductId = cartItem.ProductId,
                          Amount = cartItem.Amount,
                          CartId = cartItem.CartId
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CartItem>> GetItems(string userId)
    {
        return await (from cart in _context.Carts
                      join cartItem in _context.CartItems
                      on cart.Id equals cartItem.CartId
                      where cart.UserId == userId
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          ProductId = cartItem.ProductId,
                          Amount = cartItem.Amount,
                          CartId = cartItem.CartId
                      }).ToListAsync();
    }
}
