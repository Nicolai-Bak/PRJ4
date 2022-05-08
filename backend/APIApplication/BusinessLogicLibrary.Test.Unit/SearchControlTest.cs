using System;
using System.Collections.Generic;
using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusinessLogicLibrary.Test.Unit;

public class SearchControlTest
{
    private ISearchControl _uut;
    private IDbInsert _dbInsert;
    private IDbSearch _dbSearch;

    [SetUp]
    public void Setup()
    {
        PrisninjaDbContext context = GetDbContext();
        _dbInsert = new PrisninjaDb(context);
        _dbSearch = new PrisninjaDb(context);
        _uut = new SearchControl(CreateTestShoppingList(), _dbSearch,
            new List<IStoreSelecter>()
            {
                new CheapestStoreSelecter(),
                new ClosestStoreSelecter(),
                new BestStoreSelecter()
            });
    }

    [Test]
    public void FindStores()
    {
        var result = _uut.FindStores();
        Assert.That(result[0], Is.Empty);
        
        _dbInsert.InsertProducts(CreateTestProducts());
        _dbInsert.InsertStores(CreateTestStores());
        _dbInsert.InsertProductStores(CreateTestProductStores());

        result = _uut.FindStores();
        Assert.That(result[0], Is.Not.Empty);
    }

    private PrisninjaDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PrisninjaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new PrisninjaDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }

    private List<Product> CreateTestProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                EAN = 123,
                Name = "a",
                Brand = "",
                Units = 123,
                Measurement = "g",
                Organic = false,
                ImageUrl = ""
            }
        };
    }

    private List<Store> CreateTestStores()
    {
        return new List<Store>()
        {
            new Store()
            {
                Address = "",
                Brand = "",
                ID = 123,
                Location_X = 123,
                Location_Y = 123
            }
        };
    }

    private List<ProductStore> CreateTestProductStores()
    {
        return new List<ProductStore>()
        {
            new ProductStore()
            {
                Price = 123,
                Store = CreateTestStores()[0],
                StoreKey = CreateTestStores()[0].ID,
                Product = CreateTestProducts()[0],
                ProductKey = CreateTestProducts()[0].EAN
            }
        };
    }

    private ShoppingList CreateTestShoppingList()
    {
        return new ShoppingList()
        {
            Products = new List<ShoppingListItem>()
            {
                new ShoppingListItem()
                {
                    Name = "a",
                    Measurement = "g",
                    Organic = false,
                    Unit = 123
                }
            },
            X = 123,
            Y = 123,
            Range = 123
        };
    }
}