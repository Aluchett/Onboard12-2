using System.ComponentModel.DataAnnotations; 

namespace Onboard12.Server.ViewModels
{
    public class CreateSalesRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product is required!")]

        public int ProductId { get; set; }
        [Required(ErrorMessage = "Customer is required!")]

        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Store is required")]

        public int StoreId { get; set; }

        [Required(ErrorMessage = " Date Sold is required!")]

        public DateTime DateSold { get; set; }

    }
}
