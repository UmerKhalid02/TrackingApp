using Microsoft.EntityFrameworkCore;
using TrackingApp.Data.Entities.AuthenticationEntity;
using TrackingApp.Data.Entities.OrderEntity;
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.Seeders;

namespace TrackingApp.Data
{
    public partial class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }   
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("trk");

            modelBuilder.SeedRoles();
            modelBuilder.SeedAdminUser();
        }
    }
}
