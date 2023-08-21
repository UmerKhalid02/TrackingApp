using TrackingApp.Web.Modules.Users;
using TrackingApp.Data.IRepositories.IUserRepository;
using TrackingApp.Data.Repositories.UserRepository;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;
using TrackingApp.Data.Repositories.AuthenticationRepository.AuthenticationRepository;
using TrackingApp.Web.Modules.Authentication.Authentication;

namespace TrackingApp.Web.Extensions
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
