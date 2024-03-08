using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Api.Entities;

public class User
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;

    public Cart? Cart { get; set; }
}
