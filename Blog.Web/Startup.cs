using AutoMapper;
using Blog.Data;
using Blog.Data.Models;
using Blog.Web.Services;
using Blog.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddMvc();
            services.AddTransient<ISnapshotText, SnapshotText>();
            services.AddAutoMapper(c => { c.AddProfile(new MappingProfile()); });
            services.AddIdentity<BlogAdminUser, BlogAdminRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 20, 0);

            })
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();

            string connectionString;
            if (env.IsProduction())
            {
                connectionString = Configuration.GetConnectionString("BLOG-WEB-CONNECTIONSTRING-PROD");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("BLOG-WEB-CONNECTIONSTRING-DEV");
            }

            services.AddDbContext<Data.Models.BlogDbContext>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{permalink?}");
                
                ;
            });
        }
    }
}
