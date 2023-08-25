using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrackingApp.Application.Exceptions
{
    internal class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var file = value as IFormFile;

            if (file == null)
                return ValidationResult.Success;

            var extension = Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"The file extension {extension} is not allowed.");
            }

            return ValidationResult.Success;
        }
    }
}
