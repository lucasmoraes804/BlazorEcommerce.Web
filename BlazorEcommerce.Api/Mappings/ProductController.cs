using BlazorEcommerce.Api.Repositories;
using BlazorEcommerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Api.Mappings;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository produtoRepository)
    {
        _productRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
    {
        try
        {
            var products = await _productRepository.GetItems();
            if (products is null)
            {
                return NotFound();
            }
            else
            {
                var productDtos = products.ConvertProductsByDto();
                return Ok(productDtos);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error to acess database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetItem(int id)
    {
        try
        {
            var product = await _productRepository.GetItem(id);
            if (product is null)
            {
                return NotFound("Product not found");
            }
            else
            {
                var productDto = product.ConvertProductsByDto();
                return Ok(productDto);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error to acess database");
        }
    }

    [HttpGet]
    [Route("{categoriaId}/GetItemsByCategory")]
    public async Task<ActionResult<IEnumerable<ProductDto>>>
        GetItemsByCategory(int categoriaId)
    {
        try
        {
            var products = await _productRepository.GetItemsByCategory(categoriaId);
            var productsDto = products.ConvertProductsByDto();
            return Ok(productsDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Error to acess database");
        }
    }

    [HttpGet]
    [Route("GetCategories")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategories()
    {
        try
        {
            var categories = await _productRepository.GetCategories();
            var categoriesDto = categories.ConvertCategoriesByDto();
            return Ok(categoriesDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                                         "Error to acess database");
        }
    }
}
