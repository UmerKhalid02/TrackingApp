using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TrackingApp.Application.Exceptions;

namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class FileUploadRequestDTO
    {
        [Required]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? File { get; set; }
    }
}
