
using System.ComponentModel.DataAnnotations;

namespace Onboard12.Server.ViewModels;

     public class SalesViewModel
    {

    public int Id { get; set; }

    [Required(ErrorMessage = "Product is required!")]
    public ProductViewModel product { get; set; }

    [Required(ErrorMessage = "Customer is required!")]
    public CustomerViewModel customer { get; set; }

    [Required(ErrorMessage = "Store is required!")]
    public StoreViewModel store { get; set; }

    [Required(ErrorMessage = "Date Sold is required!")]
    public DateTime DateSold { get; set; }
}
