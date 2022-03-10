using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "State")]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-z\s]{4,50}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; } = String.Empty;
    }
}
