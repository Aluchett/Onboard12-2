using System.ComponentModel.DataAnnotations; 

namespace Onboard12.Server.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Store Name is required!")]
        [MinLength (3, ErrorMessage ="Store name must be at least 3 characters long!")]
        [StringLength(100, MinimumLength = 3)]

        public required string name { get; set; }

        [Required(ErrorMessage =" Store Address is required!")]
        [StringLength (300)]

        public required string Address { get; set; }




    }
}
