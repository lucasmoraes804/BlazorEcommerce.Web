using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorEcommerce.Models.DTOs;

namespace BlazorEcommerce.Web.Services;

public class CartBuyService : ICartBuyService
{
    private readonly HttpClient httpClient;
    public CartBuyService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public event Action<int>? OnCartBuyChanged;

    public async Task<CartItemDto> AddItem(CartItemAddsDto cartItemAddsDto)
    {
        var response = await httpClient
            .PostAsJsonAsync("api/CartBuy", 
                cartItemAddsDto);

        if (response.IsSuccessStatusCode)// status code entre 200 a 299
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // retorna o valor "padrão" ou vazio
                // para uma objeto do tipo carrinhoItemDto
                return default(CartItemDto);
            }
            //le o conteudo HTTP e retorna o valor resultante
            //da serialização do conteudo JSON para o objeto Dto
            return await response.Content.ReadFromJsonAsync<CartItemDto>();
        }

        //serializa o conteudo HTTP como uma string
        var message = await response.Content.ReadAsStringAsync();
        throw new Exception($"{response.StatusCode} Message -{message}");
    }

    public async Task<CartItemDto> UpdateAmount(CartItemUpdateAmountDto
                                                   cartItemUpdateAmountDto)
    {
        var jsonRequest = JsonSerializer.Serialize(cartItemUpdateAmountDto);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

        var response = await httpClient.PatchAsync($"api/CartBuy/{cartItemUpdateAmountDto.CartItemId}", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CartItemDto>();
        }
        return null;
    }

    public async Task<CartItemDto> DeleteItem(int id)
    {
        var response = await httpClient.DeleteAsync($"api/CartBuy/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CartItemDto>();
        }
        return default(CartItemDto);
    }

    public async Task<List<CartItemDto>> GetItems(string userId)
    {
        //envia um request GET para a uri da API CartBuy
        var response = await httpClient.GetAsync($"api/CartBuy/{userId}/GetItems");

        if (response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return Enumerable.Empty<CartItemDto>().ToList();
            }
            return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
        }

        var message = await response.Content.ReadAsStringAsync();
        throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
    }

    public void RaiseEventOnCartBuyChanged(int totalAmount)
    {
        OnCartBuyChanged?.Invoke(totalAmount);
    }
}
