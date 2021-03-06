using EShopWebApp.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-z0-9\s]{4,50}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Range(0, 999999, ErrorMessage = "{0} range must be between {1} and {2}.")]
        public int Stock { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "{0} must have a maximum of two decimal places.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "{0} out of range.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] {".jpg", ".png"})]
        public List<IFormFile> Images { get; set; } = null!;

        public string UrlImage { get; set; } = string.Empty;
        
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public string Brand { get; set; } = string.Empty;
        
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string Category { get; set; } = string.Empty; 
    }
}
