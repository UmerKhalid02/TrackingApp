using Microsoft.EntityFrameworkCore;
using TrackingApp.Application.Enums;
using TrackingApp.Data.Entities.AuthenticationEntity;

namespace TrackingApp.Data.Seeders
{
    public static class RoleSeeder
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
               .HasData(
                new Role
                {
                    RoleId = RolesKey.AdminRoleId,
                    RoleName = RolesKey.AD,
                    Description = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2023-08-01")
                },
               new Role
               {
                   RoleId = RolesKey.UserRoleId,
                   RoleName = RolesKey.US,
                   Description = "User",
                   IsActive = true,
                   CreatedAt = DateTime.Parse("2023-08-01")
               });
        }
    }
}
