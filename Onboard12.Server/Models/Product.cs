using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreReact.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product Name is required!")]
    [MinLength(3, ErrorMessage = "Product name must be at least 3 characters long!")]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Product Price is required!")]
    [Precision(18, 2)]
    public decimal Price { get; set; }


    public ICollection<Sales>? Sales { get; set; }
}

