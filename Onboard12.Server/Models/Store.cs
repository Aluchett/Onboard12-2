using System.ComponentModel.DataAnnotations;


namespace StoreReact.Models;

public class Store
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Store Name is required!")]
    [MinLength(3, ErrorMessage = "Store name must be at least 3 characters long!")]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Store Address is required!")]
    [StringLength(300)]
    public required string Address { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public ICollection<Sales>? Sales { get; set; }
}
