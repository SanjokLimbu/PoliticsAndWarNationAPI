using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PWAPI;
using PWAPI.Interface;
using PWAPI.Model;
using PWAPI.Service;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace MyWeb
{
    public class Startup
    {
        private static Timer atimer;
        public static async Task Main()
        {
            atimer = new Timer
            {
                Interval = 3600000
            };
            atimer.Elapsed += await OnTimedEventAsync();
            atimer.AutoReset = true;
            atimer.Enabled = true;
        }

        private static async Task<ElapsedEventHandler> OnTimedEventAsync()
        {
            GetAPI.InitializeClient();
            await GetNationFromAPI.GetNation();
            GetAlliancesFromApi.GetAlliance();
            throw new Exception();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<Dcontext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("connection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<Dcontext>();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = ConfigurationManager.AppSettings["ClientId"];
                    options.ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
                });

            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<INation, NationCheckList>();
            services.AddSingleton<IAlliance, AllianceCheckList>();

            // Add framework services.
            services.AddMvc();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "Logouts",
                    pattern: "{controller=Account}/{action=Logout}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Accounts",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
