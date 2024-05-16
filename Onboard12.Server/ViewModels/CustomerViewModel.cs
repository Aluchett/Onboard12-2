using System.ComponentModel.DataAnnotations;

namespace StoreReact.ViewModels;

public class CustomerViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; } 

    [Required(ErrorMessage = "Customer Name is required!")]
    [MinLength(3, ErrorMessage = "Customer name must be at least 3 characters long!")]


    public required string Address { get; set; }

    [Required(ErrorMessage = "Customer address is required!")]
    
    public DateTime Created {  get; set; }
    public DateTime Updated { get; set; }


    
}
