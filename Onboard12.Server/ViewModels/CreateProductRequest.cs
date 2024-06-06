using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;


namespace Onboard12.Server.ViewModels
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Product Name is required!")]
        [MinLength (3, ErrorMessage ="Product name must be at least 3 characters long")]
        [StringLength (100, MinimumLength = 3)]

        public required string Name { get; set; }

        [Required(ErrorMessage = "Product Price is required!")]
        [Precision (18, 2)] 

        public decimal Price { get; set; }
    }
}
