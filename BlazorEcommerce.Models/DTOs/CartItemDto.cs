﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Models.DTOs;

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public int Amount { get; set; }

    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductImageURL { get; set; }
    public decimal Price { get; set; }
    public decimal PriceTotal { get; set; }
}
