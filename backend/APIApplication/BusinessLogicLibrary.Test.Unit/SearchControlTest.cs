using System;
using System.Collections.Generic;
using System.Drawing;
using ApiApplication.Controllers;
using ApiApplication.SearchAlgorithm;
using ApiApplication.SearchAlgorithm.Models;
using BusinessLogicLibrary.SearchAlgorithm;
using DatabaseLibrary;
using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace BusinessLogicLibrary.Test.Unit;

public class SearchControlTest
{
    private ISearchControl _uut;
    private IDbInsert _dbInsert;
    private IDbSearch _dbSearch;
    private IRangeCalculator _rangeCalculator;

    [SetUp]
    public void Setup()
    {
        _rangeCalculator = new RangeCalculator();
        PrisninjaDbContext context = GetDbContext();
        IRangeCalculator rangeCalculator = new RangeCalculator();
        _dbInsert = new PrisninjaDb(context, rangeCalculator);
        _dbSearch = new PrisninjaDb(context, rangeCalculator);
        _uut = new SearchControl(CreateTestShoppingList(), _dbSearch,
            new List<IStoreSelecter>()
            {
                new CheapestStoreSelecter(),
                new ClosestStoreSelecter(),
                new BestStoreSelecter()
            }, _rangeCalculator);
    } 

    [Test]
    public void FindStores_ProductsInDatabase_ResultsNotEmpty()
    {
        var result = _uut.FindStores();
        Assert.That(result[0], Is.Empty);
        Assert.That(result[1], Is.Empty);
        Assert.That(result[2], Is.Empty);

        _dbInsert.InsertProducts(CreateTestProducts());
        _dbInsert.InsertStores(CreateTestStores());
        _dbInsert.InsertProductStores(CreateTestProductStores());

        result = _uut.FindStores();
        Assert.That(result[0], Is.Not.Empty);
        Assert.That(result[1], Is.Not.Empty);
        Assert.That(result[2], Is.Not.Empty);
    }
    
    [Test]
    public void FindStores_ProductsInDatabase_CheapestOptionFound()
    {
        var products = CreateTestProducts();
        var stores = CreateTestStores();
        var productStores = CreateTestProductStores();
        _dbInsert.InsertProducts(products);
        _dbInsert.InsertStores(stores);
        _dbInsert.InsertProductStores(productStores);

        var result = _uut.FindStores();
        Assert.That(result[0][0].StoreID, Is.EqualTo(stores[1].ID));
        Assert.That(result[0][1].StoreID, Is.EqualTo(stores[0].ID));
    }
    
    [Test]
    public void FindStores_ProductsInDatabase_NearestOptionFound()
    {
        var products = CreateTestProducts();
        var stores = CreateTestStores();
        var productStores = CreateTestProductStores();
        _dbInsert.InsertProducts(products);
        _dbInsert.InsertStores(stores);
        _dbInsert.InsertProductStores(productStores);

        var shoppingList = CreateTestShoppingList();
        IRangeCalculator rangeCalculator = Substitute.For<IRangeCalculator>();
        rangeCalculator.Distance(
            stores[0].Location_X,
            shoppingList.X,
            stores[0].Location_Y,
            shoppingList.Y).Returns(1);
        rangeCalculator.Distance(
            stores[1].Location_X,
            shoppingList.X,
            stores[1].Location_Y,
            shoppingList.Y).Returns(2);

        _uut = new SearchControl(shoppingList, _dbSearch,
            new List<IStoreSelecter>()
            {
                new CheapestStoreSelecter(),
                new ClosestStoreSelecter(),
                new BestStoreSelecter()
            }, rangeCalculator);

        var result = _uut.FindStores();
        Assert.That(result[1][0].StoreID, Is.EqualTo(stores[0].ID));
        Assert.That(result[1][1].StoreID, Is.EqualTo(stores[1].ID));
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
            },
            new Product()
            {
                EAN = 124,
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
            },
            new Store()
            {
                Address = "",
                Brand = "",
                ID = 124,
                Location_X = 456,
                Location_Y = 456
            }
        };
    }

    private List<ProductStore> CreateTestProductStores()
    {
        var products = CreateTestProducts();
        var stores = CreateTestStores();
        return new List<ProductStore>()
        {
            new ProductStore()
            {
                Price = 123,
                Store = stores[0],
                StoreKey = stores[0].ID,
                Product = products[0],
                ProductKey = products[0].EAN
            },
            new ProductStore()
            {
                Price = 10,
                Store = stores[1],
                StoreKey = stores[1].ID,
                Product = products[1],
                ProductKey = products[1].EAN
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
            Range = 999999
        };
    }
}