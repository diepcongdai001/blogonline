using BlogOnline.BackEnd;

namespace MyShop.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                    loggingBuilder.Configure(options =>
                        options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId
                                                         | ActivityTrackingOptions.SpanId)
                .AddSimpleConsole(op => op.IncludeScopes = true))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}