using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrackingApp.Data.Entities.AuthenticationEntity;

namespace TrackingApp.Data.Entities.UserEntity
{
    [Table("UserRole")]
    public class UserRole : BaseEntity
    {
        [Key]
        [Column("UserRoleID")]
        public Guid UserRoleId { get; set; }
        [Column("UserID")]
        public virtual Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Column("RoleID")]
        public virtual Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
