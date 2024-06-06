using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Onboard12.Server.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required (ErrorMessage = "Product Name is required!")]
    [MinLength(3, ErrorMessage = " Product must be at least 3 characters Long!")]
    [StringLength(100, MinimumLength = 3)]
    
    public required string Name { get; set; }
    [Required (ErrorMessage = "Product Price is Required!")]
    [Precision(18, 2)] 

    public decimal Price { get; set; } 




}
