using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingApp.Data.Entities.UserEntity
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Key]
        [Column("UserID")]
        public Guid UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public virtual UserRole UserRole { get; set;}
    }
}
