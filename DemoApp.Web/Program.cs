using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.Web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DemoApp.Web
{
  public partial class Program
  {
    public static void Main(string[] args)
    {
      CurrentDirectoryHelpers.SetCurrentDirectory();

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
          var context = services.GetRequiredService<WideWorldImportersContext>();
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
