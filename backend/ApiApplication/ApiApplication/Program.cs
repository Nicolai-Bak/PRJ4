using BusinessLogicLibrary.SearchAlgorithm;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using ExternalApiLibrary.HostedServices;
using Microsoft.EntityFrameworkCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PrisninjaDbContext>();
builder.Services.AddTransient<IDbRequest, PrisninjaDb>();
builder.Services.AddTransient<IDbSearch, PrisninjaDb>();
builder.Services.AddTransient<IDbInsert, PrisninjaDb>();

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

builder.Services.AddHostedService<ExternalApiService>();
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