using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Models.DTOs;

public class CartItemAddsDto
{
    [Required]
    public int CartId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Amount { get; set; }
}