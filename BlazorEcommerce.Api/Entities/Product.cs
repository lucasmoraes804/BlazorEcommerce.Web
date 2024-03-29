﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<CartItem> Items { get; set; }
           = new List<CartItem>();

    }
}
