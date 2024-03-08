using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Models.DTOs;

public class CartItemUpdateAmountDto
{
    public int CartItemId { get; set; }
    public int Amount { get; set; }
}
