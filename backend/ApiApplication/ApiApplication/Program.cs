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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});
<<<<<<< HEAD
builder.Services.AddHostedService(sp =>
{
    return new ExternalApiService(sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>(),
        new Dictionary<IApiFactory, IApiFactory>()
        {
            {
                new FoetexProductFactory(), new FoetexStoreFactory()
            },
            {
                new CoopProductFactory(), new CoopStoreFactory()
            }
        },
        new ProductNameStandardizer(),
        true);
});
=======
//builder.Services.AddHostedService(sp =>
//{
//    return new ExternalApiService(sp.CreateScope().ServiceProvider.GetRequiredService<IDbInsert>(),
//        new Dictionary<IApiFactory, IApiFactory>()
//        {
//            {
//                new FoetexProductFactory(), new FoetexStoreFactory()
//            },
//            {
//                new CoopProductFactory(), new CoopStoreFactory()
//            }
//        },
//        new ProductNameStandardizer(),
//        true);
//});
>>>>>>> c5b0ab7cf82e23b213a0746dac9d6c1187ce8ee5

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