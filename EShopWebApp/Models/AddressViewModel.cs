using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models
{
    public class AddressViewModel
    {
        [StringLength(100, ErrorMessage = "{0} must contain a maximum of {1} characters.")]
        public string Street { get; set; } = null!;

        [StringLength(5, ErrorMessage = "{0} must contain {1} digits.")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Only digits allowed.")]
        public string Zipcode { get; set; } = null!;

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }
    }
}
