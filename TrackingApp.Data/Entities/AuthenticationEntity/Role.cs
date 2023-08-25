using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingApp.Data.Entities.AuthenticationEntity
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        [Key]
        [Column("RoleID")]
        public Guid RoleId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string? RoleName { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }
    }
}
