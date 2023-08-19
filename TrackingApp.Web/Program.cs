namespace TrackingApp.Web 
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            using var webHost = CreateHostBuilder(args).Build();
            await webHost.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}