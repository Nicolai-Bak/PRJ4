using ApiApplication.Database;
using ApiApplication.Database.Data;
using ApiApplication.Database.Models;
using Microsoft.EntityFrameworkCore;

//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
//
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();

using (var context = new PrisninjaDbContext())
{
    Store newStore = new Store()
    {
        ID = 93,
        Brand = 3,
        Location_X = (float) 12.5,
        Location_Y = (float) 1.2,
        Address = "vejen 14",
        Products = new List<Product>()
    };
    
    Product newProduct = new Product()
    {
        Name = "æg",
        Brand = "jkdla",
        Unit = 10,
        Measurement = "stk",
        Price = 10,
        Stores = new List<Store>()
    };
    
    newProduct.Stores.Add(newStore);
    // newStore.Products.Add(newProduct);

    context.Add(newStore);
    context.Add(newProduct);
    
    context.Database.OpenConnection();
    try
    {
        context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT Stores ON");
        context.SaveChanges();
        context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT Stores OFF");
    }
    finally
    {
        context.Database.CloseConnection();
    }

    Console.WriteLine("hello\n");
}

//app.Run();