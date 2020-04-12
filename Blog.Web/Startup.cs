using AutoMapper;
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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            
            string appInsightsKey = Configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");
            if(appInsightsKey != null) {
                services.AddApplicationInsightsTelemetry(appInsightsKey);
            }

            services.AddMvc(config=> {
                //config.Filters.Add(typeof(CustomAuthorizationFilter));
                config.EnableEndpointRouting = false;
            })
            .AddRazorRuntimeCompilation();
                
            services.AddTransient<ISnapshotText, SnapshotText>();
            services.AddTransient<IDatabaseCreator, DatabaseCreator>();
            services.AddTransient<DataSeeder>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddIdentity<BlogAdminUser, BlogAdminRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 20, 0);
                options.User.RequireUniqueEmail = true;

            })
            .AddEntityFrameworkStores<BlogDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };
                options.AccessDeniedPath = new PathString("/Forbidden");
                options.LoginPath = new PathString("/Account/SignIn");
                options.LogoutPath = new PathString("/Account/SignOut");
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                
            });

            string connectionString;
            if(CurrentEnvironment.IsDevelopment())
            {
                 connectionString = @"Server=(localdb)\mssqllocaldb;Database=solores-dev-sql-db;Trusted_Connection=True;";
            } 
            else
            {
                connectionString = Configuration.GetConnectionString("BLOG_CONNECTIONSTRING");
            }

            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BlogDbContext dbContext, IDatabaseCreator databaseCreator)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{permalink?}");
                
            });

            if(!CurrentEnvironment.IsDevelopment()) 
            {
                string connectionString = Configuration.GetConnectionString("BLOG_CONNECTIONSTRING");
                databaseCreator.CreateDatabase(connectionString); // Create DB manually if not created already, to stop Azure creating a super expensive one
            }

            dbContext.Database.Migrate();
            
            DataSeeder.Run(app.ApplicationServices).Wait();

        }
    }
}
