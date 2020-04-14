using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public class DataSeeder
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<BlogAdminRole> _roleManager;
        private readonly UserManager<BlogAdminUser> _userManager;
        private readonly BlogDbContext _dbContext;

        public DataSeeder(IConfiguration configuration, RoleManager<BlogAdminRole> roleManager, UserManager<BlogAdminUser> userManager, BlogDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public static async Task Run(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var instance = serviceScope.ServiceProvider.GetService<DataSeeder>();
                await instance.SeedDefaultRole();
                await instance.SeedDefaultUser();
                await instance.SeedPosts();
            }
        }

        public async Task SeedPosts()
        {
            if (!_dbContext.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        CreationDate = new System.DateTime(2019,01,01),
                        Html = "<p>Hello! I'm Mike, a .NET-oriented developer, and this is my tech blog. I'm not usually the blogging type but as a developer you accumulate so much random knowledge that often gets lost or forgotten - it seemed like it was time to start recording some of it, so I can refer back to it, and if it helps anyone else out there even better :) <br/> This is my own blog site that I made for myself in ASP.NET Core MVC and hosted on Azure. It has a few useful features, like a simple search function, and a protected Admin section where the blog site owner can create, save (and in future edit) blog posts. I hope to add new features in the future. <br/><br/> The code hosted on GitHub <a href=\"https://github.com/zola-25/Blog\">here</a>. </p>",
                        Title = "New Blog Site",
                        UrlSegment = "NewBlog"
                    }
                };
                _dbContext.AddRange(posts);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SeedDefaultRole()
        {
            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new BlogAdminRole();
                role.Name = "Administrator";
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedDefaultUser()
        {
            var adminEmail = _configuration.GetValue<string>("Blog-Admin-Email");

            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new BlogAdminUser();
                user.UserName = adminEmail;
                user.Email = adminEmail;

                var adminPassword = _configuration.GetValue<string>("Blog-Admin-Password");
                
                var result = await _userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                    throw new Exception("Could not seed default user");
            }

        }

    }

}
