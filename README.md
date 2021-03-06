# actifio-demo

## Running on IIS

```
choco install dotnetcore-runtime.install
choco install dotnetcore-windowshosting
```

## Scaffold from Database

```
dotnet ef dbcontext scaffold 'Server=localhost;User ID=sa;Database=WideWorldImporters;Password=<password>' Microsoft.EntityFrameworkCore.SqlServer -o Data
```

## Scaffold Pages

```
    public class WideWorldImportersContextFactory : IDesignTimeDbContextFactory<WideWorldImportersContext>
    {
        public WideWorldImportersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WideWorldImportersContext>();

            optionsBuilder.UseSqlServer("Server=localhost;User ID=sa;Database=WideWorldImporters;Password=demo01!password", x => x.UseNetTopologySuite());

            return new WideWorldImportersContext(optionsBuilder.Options);
        }
    }
```

```
dotnet aspnet-codegenerator razorpage -m Models.People -dc DemoApp.Web.Data.WideWorldImportersContext -udl -outDir Pages/People --referenceScriptLibraries
```
