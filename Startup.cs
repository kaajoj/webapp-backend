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
using VSApi.Data;
using VSApi.Interfaces;
using VSApi.Models;
using VSApi.Services;

namespace VSApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var aTimer = new Timer
            {
                Interval = 1000 * 30 * 30
            };
            aTimer.Elapsed += OnTimedEventAPI;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            var aTimer2 = new Timer
            {
                Interval = (1000 * 30 * 30) + 10000
            };
            aTimer2.Elapsed += OnTimedEventEmails;
            aTimer2.AutoReset = true;
            aTimer2.Enabled = true;
        }

        private static void OnTimedEventAPI(Object source, ElapsedEventArgs e)
        {
            var urlUpdateApi = new UriBuilder("https://localhost:5001/api/crypto/getcmcapi");
            var urlUpdateWallet = new UriBuilder("https://localhost:5001/api/wallet/edit/prices/");

            var client = new WebClient();
            client.DownloadString(urlUpdateApi.ToString());
            client.DownloadString(urlUpdateWallet.ToString());

            Console.WriteLine("API refreshed at {0}", e.SignalTime);
        }

        private static void OnTimedEventEmails(Object source, ElapsedEventArgs e)
        {
            var urlCheckAlerts = new UriBuilder("https://localhost:5001/api/wallet/check/alerts/");
            var client = new WebClient();
            client.DownloadString(urlCheckAlerts.ToString());

            Console.WriteLine("Email sent at {0}", e.SignalTime);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            // Add ApplicationDbContext.
            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("connectionString"))
            );

            // DI
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<ICoinMarketCapApiService, CoinMarketCapApiService>();
            services.AddScoped<IWalletOperationsService, WalletOperationsService>();

            services.AddScoped<ICryptoRepository, CryptoRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

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

            services.AddControllers();
            services.AddRazorPages();
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

            app.UseStaticFiles();

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
