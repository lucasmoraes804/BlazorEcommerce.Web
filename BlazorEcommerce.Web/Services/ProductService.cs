using BlazorEcommerce.Web.Services;
using BlazorEcommerce.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorEcommerce.Web.Services;

public class ProductService : IProductService
{
    public HttpClient _httpClient;
    public ILogger<ProductService> _logger;

    public ProductService(HttpClient httpClient,
        ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetItems()
    {
        try
        {
            var productsDto = await _httpClient.
                             GetFromJsonAsync<IEnumerable<ProductDto>>("api/Products");
            return productsDto;
        }
        catch (Exception)
        {
            _logger.LogError("Erro ao acessar produtos : api/Products ");
            throw;
        }
    }

    public async Task<ProductDto> GetItem(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro a obter produto pelo id= {id} - {message}");
                throw new Exception($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception)
        {
            _logger.LogError($"Erro a obter produto pelo id={id}");
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Products/GetCategories");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CategoryDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoriaId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Products/{categoriaId}/GetItemsByCategory");
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<ProductDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status Code - {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception)
        {
            //log exception
            throw;
        }
    }
}
