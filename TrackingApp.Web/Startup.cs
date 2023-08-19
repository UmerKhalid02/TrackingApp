using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackingApp.Application;
using TrackingApp.Data;
using TrackingApp.Web.Extensions;

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
            

            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddControllers();
            services.AddSwaggerGen();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
