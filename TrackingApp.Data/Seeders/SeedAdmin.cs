using Microsoft.EntityFrameworkCore;
using TrackingApp.Application.Enums;
using TrackingApp.Data.Entities.UserEntity;

namespace TrackingApp.Data.Seeders
{
    public static class SeedAdmin
    {
        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                new User
                {
                    UserId = Guid.Parse("EF12EE01-ADCF-4A8A-8544-03A592D9E252"),
                    Name = "Admin",
                    UserName = "admin",
                    Password = "admin1234", //admin1234
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2023-08-01"),
                });

            modelBuilder.Entity<UserRole>()
                .HasData(
                new UserRole
                { 
                    UserRoleId = Guid.Parse("70CDB88C-CA74-48E0-A597-162479301C9E"),
                    UserId = Guid.Parse("EF12EE01-ADCF-4A8A-8544-03A592D9E252"),
                    RoleId = RolesKey.AdminRoleId,
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2023-08-01"),
                });
                       
        }
    }
}
