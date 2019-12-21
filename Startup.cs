using App.API.Models;
using App.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Timers;
using System.Net;
using System.Web;

namespace App.API
{
    public class Startup
    {
        private string _connectionString = null;
        private static Timer aTimer;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            aTimer = new System.Timers.Timer();
            // aTimer.Interval = 1000*60*15;
            aTimer.Interval = 1000*15;

            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var URLUpdateApi = new UriBuilder("https://localhost:5001/api/crypto/getcmcapi");
            var URLUpdateWallet = new UriBuilder("https://localhost:5001/api/wallet/edit/prices/");

            var client = new WebClient();
            client.DownloadString(URLUpdateApi.ToString());
            client.DownloadString(URLUpdateWallet.ToString());
            
            Console.WriteLine("API refreshed at {0}", e.SignalTime);

            Emails emails = new Emails();
            emails.prepareMessage();
            Console.WriteLine("Email sent");
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


            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApiContext>(
                opt => opt.UseNpgsql(_connectionString));

            services.AddTransient<DataSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeed seed)
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

            app.UseRouting(routes =>
            {
                routes.MapApplication();
            });

            app.UseAuthorization();

            // seed.SeedData();

            app.UseMvc(routes => routes.MapRoute(
                "default", "api/{controller}/{action}/{id?}"
            ));
        }
    }
}
