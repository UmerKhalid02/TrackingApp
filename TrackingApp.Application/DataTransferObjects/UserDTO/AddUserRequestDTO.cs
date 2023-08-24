using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.Application.DataTransferObjects.UserDTO
{
    public class AddUserRequestDTO
    {
        [Required(ErrorMessage = "Username is Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be atleast 3 chars long")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password length must be between 8 and 15 characters")]
        public string? Password { get; set; }
        public string? ContactNo { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
