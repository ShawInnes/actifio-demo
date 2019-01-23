using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace DemoApp.Web
{
    public static class Logging
    {
        public static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", configuration["ENVIRONMENT"])
                .Enrich.WithProperty("Application", configuration["applicationName"])
                .CreateLogger();

            Log.Information("Info {@section}", configuration.GetValue<object>("Serilog"));
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddConfiguration(configuration)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Logging.ConfigureLogging(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = Configuration.GetValue<string>("ConnectionString");
            var databaseType = Configuration.GetValue<string>("DatabaseType");

            Log.Information("Using {databaseType} Database", databaseType);
            if (databaseType == "SqlServer")
            {
                services.AddDbContext<WideWorldImportersContext>(options => options.UseSqlServer(connectionString,
                    x => x.UseNetTopologySuite()));
            }
            else if (databaseType == "Postgresql")
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<WideWorldImportersContext>(options => options.UseNpgsql(connectionString,
                    x => x.UseNetTopologySuite()));
            }
            else
            {
                Log.Warning("No Valid Database Type Specified");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
