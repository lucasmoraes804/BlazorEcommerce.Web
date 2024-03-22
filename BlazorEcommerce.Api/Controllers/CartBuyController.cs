using BlazorEcommerce.Api.Mappings;
using BlazorEcommerce.Api.Repositories;
using BlazorEcommerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartBuyController : ControllerBase
{
    private readonly ICartBuyRepository cartBuyRepo;
    private readonly IProductRepository productRepo;

    private ILogger<CartBuyController> logger;

    public CartBuyController(ICartBuyRepository
        cartBuyRepository,
        IProductRepository productRepository,
        ILogger<CartBuyController> logger)
    {
        cartBuyRepo = cartBuyRepository;
        productRepo = productRepository;
        this.logger = logger;
    }

    [HttpGet]
    [Route("{usuarioId}/GetItems")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(string userId)
    {
        try
        {
            var cartItems = await cartBuyRepo.GetItems(userId);
            if (cartItems == null)
            {
                return NoContent(); // 204 Status Code
            }

            var products = await this.productRepo.GetItems();
            if (products == null)
            {
                throw new Exception("Não existem produtos...");
            }

            var cartItemsDto = cartItems.ConvertCartItemsByDto(products);
            return Ok(cartItemsDto);
        }
        catch (Exception ex)
        {
            logger.LogError("## Erro ao obter itens do carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CartItemDto>> GetItem(int id)
    {
        try
        {
            var cartItem = await cartBuyRepo.GetItem(id);
            if (cartItem == null)
            {
                return NotFound($"Item não encontrado"); //404 status code
            }

            var product = await productRepo.GetItem(cartItem.ProductId);

            if (product == null)
            {
                return NotFound($"Item não existe na fonte de dados");
            }
            var cartItemDto = cartItem.ConvertCartItemByDto(product);

            return Ok(cartItemDto);
        }
        catch (Exception ex)
        {
            logger.LogError($"## Erro ao obter o item ={id} do carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CartItemDto>> PostItem([FromBody]
    CartItemAddsDto CartItemAddsDto)
    {
        try
        {
            var newCartItem = await cartBuyRepo.AddItem(CartItemAddsDto);

            if (newCartItem == null)
            {
                return NoContent(); //Status 204
            }

            var product = await productRepo.GetItem(newCartItem.ProductId);

            if (product == null)
            {
                throw new Exception($"Produto não localizado (Id:({CartItemAddsDto.ProductId})");
            }

            var newCartItemDto = newCartItem.ConvertCartItemByDto(product);

            return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id },
                newCartItemDto);

        }
        catch (Exception ex)
        {
            logger.LogError("## Erro criar um novo item no carrinho");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
    {
        try
        {
            var cartItem = await cartBuyRepo.DeleteItem(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            var product = await productRepo.GetItem(cartItem.ProductId);

            if (product is null)
                return NotFound();

            var CartItemDto = cartItem.ConvertCartItemByDto(product);
            return Ok(CartItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CartItemDto>> UpdateAmount(int id, 
        CartItemUpdateAmountDto CartItemUpdateAmountDto)
    {
        try
        {

            var cartItem = await cartBuyRepo.UpdateAmount(id, 
                                   CartItemUpdateAmountDto);

            if (cartItem == null)
            {
                return NotFound();
            }
            var product = await productRepo.GetItem(cartItem.ProductId);
            var CartItemDto = cartItem.ConvertCartItemByDto(product);
            return Ok(CartItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
