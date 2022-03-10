using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-z\s]{5,50}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; } = null!;
    }
}
