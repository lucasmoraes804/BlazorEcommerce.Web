namespace BlazorEcommerce.Api.Entities;

public class Cart
{
    public int Id { get; set; }
    public required string UserId { get; set; }

    public ICollection<CartItem> Itens { get; set; } =
        new List<CartItem>();
}

