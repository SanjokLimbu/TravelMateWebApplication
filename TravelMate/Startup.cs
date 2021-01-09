using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;
using TravelMate.ModelFolder.IdentityModel;
using TravelMate.Service;

namespace TravelMate
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.Configure<IServiceProvider>(options => {
                    options.GetService<AppDbContext>().Database.Migrate();
                }).AddDbContextPool<AppDbContext>(options => {
                    options.UseSqlServer(_config.GetConnectionString("AzureSqlConnection"));
                    //options.EnableSensitiveDataLogging(true);
                });
            }
            else
            {
                services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("Connection")));
            }
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            services.AddTransient<IMailService, SendGridMailService>();
            services.AddHostedService<TimedHostedServices>();
            services.AddScoped<IGetGlobalCovidData, GetGlobalCovidData>();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddAntiforgery(options => options.HeaderName = "XSRF-TOKEN");
            //ServicePointManager.ServerCertificateValidationCallback += //This code is security risk as it validates all certificates
            //    (sender, certificate, chain, errors) =>                 //Not to be used for production and used this instance as I trust the 
            //    {                                                       //The site I'm pulling data from
            //        return errors == SslPolicyErrors.None;
            //    };
            //services.AddLogging(loggingBuilder => {                                             //This code is security risk as it displays all sensitive data
            //    loggingBuilder.AddConsole()                                                     //Not recommended for production
            //        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            //    loggingBuilder.AddDebug();
            //});
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
