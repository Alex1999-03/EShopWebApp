using System.ComponentModel.DataAnnotations;

namespace EShopWebApp.Models.Validations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;
            if(files != null)
            {
                foreach(var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult($"Only {_extensions[0]} and {_extensions[1]} extensions allowed.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
