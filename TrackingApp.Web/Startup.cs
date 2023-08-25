using Microsoft.EntityFrameworkCore;
using TrackingApp.Application;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Data;
using TrackingApp.Web.Extensions;
using TrackingApp.Web.Middlewares;

namespace TrackingApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddSingleton<NLogTrack, NLogTrack>();

            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddSwaggerGen();

            // This is Settings for AWS S3
            var S3SettingsSection = Configuration.GetSection("AWSS3");
            services.Configure<AWSS3Model>(S3SettingsSection);
            var S3Settings = S3SettingsSection.Get<AWSS3Model>();

            //This is Settings for the JWT Secret Key
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddJwtTokenAuthentication(Configuration, key);
            services.AddHttpContextAccessor();

            // For Entity Framework  
            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringMssql")));

            services.AddCors();
            services.AddServicesConfig(Configuration);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackingApp.Web v1"));
            }

            app.UseRouting();

            app.UseCors(x => x
                 .WithOrigins() // provide origins here
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
