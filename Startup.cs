using App.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Timers;
using System.Net;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    public class Startup
    {
        private string _connectionString = null;
        private static Timer aTimer;
        private static Timer aTimer2;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000*30*30;
            aTimer.Elapsed += OnTimedEventAPI;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            aTimer2 = new System.Timers.Timer();
            aTimer2.Interval = (1000*30*30)+10000;
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
                c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            _connectionString = Configuration.GetConnectionString("connectionString");


            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson();

            services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApiContext>(
                opt => opt.UseNpgsql(_connectionString));

            services.AddTransient<DataSeed>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // seed.SeedData();

            app.UseMvc(routes => routes.MapRoute(
                "default", "api/{controller}/{action}/{id?}"       
            ));
        }
    }
}
