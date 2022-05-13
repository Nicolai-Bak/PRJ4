using BusinessLogicLibrary.ProductNameStandardize;
using BusinessLogicLibrary.SearchAlgorithm;
using BusinessLogicLibrary.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm.Models.Interfaces;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using ExternalApiLibrary;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.HostedServices;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PrisninjaDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer("" +
                                "Server=tcp:prisninjadb.database.windows.net,1433;" +
                                "Initial Catalog=PrisninjaWebApiDb;" +
                                "Persist Security Info=False;" +
                                "User ID=PrisninjaDb;" +
                                "Password=PRJ4Server;" +
                                "MultipleActiveResultSets=False;" +
                                "Encrypt=True;" +
                                "TrustServerCertificate=False;" +
                                "Connection Timeout=30;"
    );
});
builder.Services.AddScoped<IRangeCalculator, RangeCalculator>();
builder.Services.AddScoped<IDbRequest, PrisninjaDb>();
builder.Services.AddScoped<IDbSearch, PrisninjaDb>();
builder.Services.AddScoped<IDbInsert, PrisninjaDb>();

// builder.Services.AddHostedService(sp =>
// {
//     return new ExternalApiService(sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>(),
//         new Dictionary<IApiFactory, IApiFactory>()
//         {
//             {
//                 new FoetexProductFactory(), new FoetexStoreFactory()
//             },
//             {
//                 new CoopProductFactory(), new CoopStoreFactory()
//             }
//         },
//         new ProductNameStandardizer(),
//         true);
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}   

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();