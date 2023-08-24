using TrackingApp.Web.Modules.Users;
using TrackingApp.Data.IRepositories.IUserRepository;
using TrackingApp.Data.Repositories.UserRepository;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;
using TrackingApp.Data.Repositories.AuthenticationRepository.AuthenticationRepository;
using TrackingApp.Web.Modules.Authentication.Authentication;
using TrackingApp.Web.Modules.Orders;
using TrackingApp.Data.IRepositories.IOrderRepository;
using TrackingApp.Data.Repositories.OrderRepository;

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
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
