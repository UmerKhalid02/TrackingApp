using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Exceptions;

namespace TrackingApp.Application.DataTransferObjects.OrderDTO
{
    public class UpdateOrderRequestDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? OrderName { get; set; } // OrderName is the name of the product
        public string? DesignNo { get; set; }
        public string? Vendor { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1")]
        public int Quantity { get; set; }
        public string? OrderTaker { get; set; }

        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? OrderImage { get; set; }

        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? StitchingImage { get; set; }
    }
}
