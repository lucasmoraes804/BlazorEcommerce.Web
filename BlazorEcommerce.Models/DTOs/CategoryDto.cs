using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Models.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? IconCSS { get; set; }

}