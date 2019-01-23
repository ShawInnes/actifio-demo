using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using DemoApp.Web.Data;

namespace DemoApp.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
          .UseKestrel()
          .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
          .UseIISIntegration()
          .UseSerilog()
          .UseStartup<Startup>()
          .Build();

      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        try
        {
          var context = services.GetRequiredService<BloggingContext>();
          context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
          Log.Fatal(ex, "An error occurred creating the DB.");
        }
      }

      host.Run();
    }
  }
}