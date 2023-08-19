using TrackingApp.Web.Modules.Users;
using TrackingApp.Data.IRepositories.IUserRepository;
using TrackingApp.Data.Repositories.UserRepository;

namespace TrackingApp.Web.Extensions
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
