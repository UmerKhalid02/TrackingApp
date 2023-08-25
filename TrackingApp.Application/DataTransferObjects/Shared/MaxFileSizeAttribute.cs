using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var file = value as IFormFile;

            if (file == null)
                return ValidationResult.Success;

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"The file size should not be greater than {_maxFileSize / (1024 * 1024) } MB.");
            }

            return ValidationResult.Success;
        }
    }
}
