using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public static class DatabaseSeedInitializer
    {
        public static async Task<IWebHost> Seed(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                
                try
                {
                    var dbContext = scope.ServiceProvider.GetService<BlogDbContext>();
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<BlogRole>>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<BlogUser>>();


                    var dataSeeder = new DataSeeder();
                    dataSeeder.SeedPosts(dbContext);
                    await dataSeeder.SeedRoles(roleManager);
                    await dataSeeder.SeedUsers(userManager);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return host;
        }
    }

}
