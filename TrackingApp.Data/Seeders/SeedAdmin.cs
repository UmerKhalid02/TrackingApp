using Microsoft.EntityFrameworkCore;
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
                    UserName = "admin",
                    Password = "$admin1234*",
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2023-08-01"),
                }); 
                       
        }
    }
}
