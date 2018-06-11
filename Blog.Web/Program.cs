using Blog.Data.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Blog
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Seed().Wait();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

       
    }
}
