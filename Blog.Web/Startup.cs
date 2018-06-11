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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ISnapshotText, SnapshotText>();
            services.AddAutoMapper(c=> { c.AddProfile(new MappingProfile()); });
            services.AddIdentity<BlogUser, BlogRole>()
                    .AddEntityFrameworkStores<BlogDbContext>()
                    .AddDefaultTokenProviders();

            var connection = Configuration.GetConnectionString("BlogDatabase");
            services.AddDbContext<Data.Models.BlogDbContext>(options => options.UseSqlServer(connection));
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
