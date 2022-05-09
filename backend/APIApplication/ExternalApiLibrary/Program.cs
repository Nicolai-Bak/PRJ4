using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.HostedServices;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ExternalApiLibrary;

public static class Program
{
    public static async Task Main()
    {
        IDbInsert db = new PrisninjaDb(new PrisninjaDbContext(
            new DbContextOptionsBuilder<PrisninjaDbContext>()
                .UseSqlServer("" +
                              "Server=tcp:prisninjadb.database.windows.net,1433;" +
                              "Initial Catalog=PrisninjaWebApiDb;" +
                              "Persist Security Info=False;" +
                              "User ID=PrisninjaDb;" +
                              "Password=PRJ4Server;" +
                              "MultipleActiveResultSets=False;" +
                              "Encrypt=True;" +
                              "TrustServerCertificate=False;" +
                              "Connection Timeout=30;")
                .Options), new RangeCalculator());

        ExternalApiService externalApiService = new ExternalApiService(db,
            new List<IApiFactory[]>()
            {
                new IApiFactory[2]
                {
                    new FoetexProductFactory(),
                    new FoetexStoreFactory()

                },
                new IApiFactory[2]
                {
                    new CoopProductFactory(),
                    new CoopStoreFactory()
                },
            },
            new ProductNameStandardizer(),
            false);

        await externalApiService.UpdateDatabase();
    }
}