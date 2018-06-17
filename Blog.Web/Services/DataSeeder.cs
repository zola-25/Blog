using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public class DataSeeder
    {
        public void SeedPosts(BlogDbContext context)
        {
            if (!context.Posts.Any())
            {
                var posts = new List<Post>
                {
                    new Post
                    {
                        CreationDate = new System.DateTime(2018,01,01),
                        Html = "<h2>New Blogging Site</h2><p>This is the seed data of the new blog site</p>",
                        Title = "New Blog Site"
                    }
                };
                context.AddRange(posts);
                context.SaveChanges();
            }
        }

        public async Task SeedDefaultRole(RoleManager<BlogAdminRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new BlogAdminRole();
                role.Name = "Administrator";
                await roleManager.CreateAsync(role);
            }
        }

        public async Task SeedDefaultUser(UserManager<BlogAdminUser> userManager)
        {
            if (await userManager.FindByNameAsync("DefaultUser") == null)
            {
                var user = new BlogAdminUser();
                user.UserName = "DefaultUser";
                user.Email = "defaultuser@localhost";

                var result = await userManager.CreateAsync(user, "Password_123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                    throw new Exception("Could not see default user");
            }

        }

    }

}
