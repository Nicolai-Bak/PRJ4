using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PrisninjaDbContext>();
builder.Services.AddScoped<IPrisninjaDB, PrisninjaDb>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Product p = new Product()
// {
//     EAN = 6571938,
//     Name = "æ",
//     Brand = "jdlwajkdla",
//     Unit = 110,
//     Measurement = "s24",
//     Price = 102
// };

// PrisninjaDb db = new PrisninjaDb(new PrisninjaDbContext());
// await db.InsertProduct(p, 2, 1.5);
// List<string> names = db.GetAllProductNames();
// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }

// List<Product> products = db.GetProductsFromSpecificStores(new List<int>() { 1 }, "k");
//
// foreach (var p in products)
// {
//     Console.WriteLine(p.Name);
// }