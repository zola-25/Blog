using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Services
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

        public async Task SeedRoles(RoleManager<BlogRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new BlogRole();
                role.Name = "Administrator";
                await roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsers(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByNameAsync("DefaultUser") == null)
            {
                var user = new BlogUser();
                user.UserName = "DefaultUser";
                user.Email = "defaultuser@localhost";

                var result = await userManager.CreateAsync(user, "password_goes_here");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

        }

    }

}
