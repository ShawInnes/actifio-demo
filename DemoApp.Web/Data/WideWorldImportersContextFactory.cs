using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DemoApp.Web.Data
{
    public class WideWorldImportersContextFactory : IDesignTimeDbContextFactory<WideWorldImportersContext>
    {
        public WideWorldImportersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WideWorldImportersContext>();

            optionsBuilder.UseSqlServer("Server=localhost;User ID=sa;Database=WideWorldImporters;Password=demo01!password", x => x.UseNetTopologySuite());

            return new WideWorldImportersContext(optionsBuilder.Options);
        }
    }
}