using System;
using System.Net;
using System.Timers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VSApi.Models;

namespace VSApi
{
    public class Startup
    {
        private string _connectionString = null;
        private static Timer aTimer;
        private static Timer aTimer2;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            aTimer = new Timer
            {
                Interval = 1000 * 30 * 30
            };
            aTimer.Elapsed += OnTimedEventAPI;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            aTimer2 = new Timer
            {
                Interval = (1000 * 30 * 30) + 10000
            };
            aTimer2.Elapsed += OnTimedEventEmails;
            aTimer2.AutoReset = true;
            aTimer2.Enabled = true;
        }

        private static void OnTimedEventAPI(Object source, System.Timers.ElapsedEventArgs e)
        {
            var URLUpdateApi = new UriBuilder("https://localhost:5001/api/crypto/getcmcapi");
            var URLUpdateWallet = new UriBuilder("https://localhost:5001/api/wallet/edit/prices/");

            var client = new WebClient();
            client.DownloadString(URLUpdateApi.ToString());
            client.DownloadString(URLUpdateWallet.ToString());
            
            Console.WriteLine("API refreshed at {0}", e.SignalTime);
        }

           private static void OnTimedEventEmails(Object source, System.Timers.ElapsedEventArgs e)
        {
            var URLCheckAlerts = new UriBuilder("https://localhost:5001/api/wallet/check/alerts/");
            var client = new WebClient();
            client.DownloadString(URLCheckAlerts.ToString());

            Console.WriteLine("Email sent at {0}", e.SignalTime);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy",
                c => c
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            _connectionString = Configuration.GetConnectionString("connectionString");

            // Add EntityFramework support for SqlServer.
            services.AddEntityFrameworkSqlServer();

            // Add ApplicationDbContext.
            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(_connectionString)
            );


            // PostgreSQL
            // services.AddEntityFrameworkNpgsql().AddDbContext<ApiContext>((sp, options) =>
            // {
            //     options.UseNpgsql(_connectionString);
            //     options.UseInternalServiceProvider(sp);
            //         
            // });

            // Add ASP.NET Core Identity support
            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApiContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApiContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            // services.AddTransient<DataSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            // seed.SeedData();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

        }
    }
}
