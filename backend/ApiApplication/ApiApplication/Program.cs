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

// using (var context = new PrisninjaDbContext())
// {
//     Store newStore = new Store()
//     {
//         Brand = 3,
//         Location_X = 12.5,
//         Location_Y = 1.2,
//         Address = "vejen 14"
//     };
//     
//     Product newProduct = new Product()
//     {
//         EAN = 389021,
//         Name = "æg",
//         Brand = "jkdla",
//         Unit = 10,
//         Measurement = "stk",
//         Price = 10
//     };
//
//     ProductStore newPS = new ProductStore()
//     {
//         Product = newProduct,
//         Store = newStore,
//         Price = 13.5
//     };
//
//     context.Add(newStore);
//     context.Add(newProduct);
//     context.Add(newPS);
//     
//     context.Database.OpenConnectionAsync();
//     try
//     {
//         context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Products ON");
//         context.SaveChangesAsync();
//         context.Database.ExecuteSqlInterpolatedAsync($"SET IDENTITY_INSERT Products OFF");
//     }
//     finally
//     {
//         context.Database.CloseConnectionAsync();
//     }
//
//     Console.WriteLine("hello\n");
// }

PrisninjaDb db = new PrisninjaDb(new PrisninjaDbContext());
await db.InsertProduct(new Product(), new Store(), 12);

//app.Run();