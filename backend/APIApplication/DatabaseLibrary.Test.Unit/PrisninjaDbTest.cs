using System;
using System.Collections.Generic;
using DatabaseLibrary.Data;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DatabaseLibrary.Test;

public class PrisninjaDbTest
{
    private PrisninjaDbContext context;
    private PrisninjaDb uut;

    [SetUp]
    public void Setup()
    {
        context = GetDbContext();
        uut = new PrisninjaDb(context, new RangeCalculator());
    }

    [Test]
    public void GetAllProducts_CorrectProductReturned()
    {
        Product p1 = new Product()
        {
            Brand = "",
            EAN = 1,
            ImageUrl = "",
            Measurement = "",
            Name = "p1",
            Organic = false,
            Units = 100
        };
        context.Products.Add(p1);
        context.SaveChanges();

        Assert.IsTrue(uut.GetAllProducts().Contains(p1));
    }
    
    [Test]
    public void GetAllProductNames_CorrectProductNameReturned()
    {
        ProductStandardName psn1 = new ProductStandardName()
        {
            Name = "psn1",
            Organic = false,
            MeasureG = false,
            MeasureL = false,
            MeasureStk = false
        };
        context.ProductStandardNames.Add(psn1);
        context.SaveChanges();

        Assert.IsTrue(uut.GetAllProductNames().Contains(psn1.Name));
    }
    
    [Test]
    public void GetProductInfo_CorrectProductStandardNameReturned()
    {
        ProductStandardName psn1 = new ProductStandardName()
        {
            Name = "psn1",
            Organic = false,
            MeasureG = false,
            MeasureL = false,
            MeasureStk = false
        };
        context.ProductStandardNames.Add(psn1);
        context.SaveChanges();

        Assert.That(uut.GetProductInfo(psn1.Name), Is.EqualTo(psn1));
    }
    
    [Test]
    public void GetStoresInRange_CorrectStoreIdsReturned()
    {
        Store s1 = new Store()
        {
            ID = 1,
            Brand = "",
            Address = "",
            Location_X = 5,
            Location_Y = 5
        };
        Store s2 = new Store()
        {
            ID = 2,
            Brand = "",
            Address = "",
            Location_X = 8,
            Location_Y = 8
        };
        Store s3 = new Store()
        {
            ID = 3,
            Brand = "",
            Address = "",
            Location_X = 9,
            Location_Y = 9
        };
        context.Stores.Add(s1);
        context.Stores.Add(s2);
        context.Stores.Add(s3);
        context.SaveChanges();

        Assert.IsTrue(uut.GetStoresInRange(5,5, 5).Contains(s1.ID));
        Assert.IsTrue(uut.GetStoresInRange(5,5, 5).Contains(s2.ID));
        Assert.IsFalse(uut.GetStoresInRange(5,5, 5).Contains(s3.ID));
    }
    
    [Test]
    public void GetDataFromStores_CorrectStoresReturned()
    {
        Store s1 = new Store()
        {
            ID = 1,
            Brand = "",
            Address = "",
            Location_X = 10,
            Location_Y = 5
        };
        Store s2 = new Store()
        {
            ID = 2,
            Brand = "",
            Address = "",
            Location_X = 8,
            Location_Y = 8
        };
        Store s3 = new Store()
        {
            ID = 3,
            Brand = "",
            Address = "",
            Location_X = 9,
            Location_Y = 9
        };
        List<int> ids = new List<int>() { s1.ID, s2.ID };
        
        context.Stores.Add(s1);
        context.Stores.Add(s2);
        context.Stores.Add(s3);
        context.SaveChanges();

        Assert.IsTrue(uut.GetDataFromStores(ids).Contains(s1));
        Assert.IsTrue(uut.GetDataFromStores(ids).Contains(s2));
        Assert.IsFalse(uut.GetDataFromStores(ids).Contains(s3));
    }
    
    [Test]
    public void GetProductsFromSpecificStores_CorrectProductsReturned()
    {
        Store s1 = new Store()
        {
            ID = 1,
            Brand = "",
            Address = "",
            Location_X = 10,
            Location_Y = 5
        };
        Store s2 = new Store()
        {
            ID = 2,
            Brand = "",
            Address = "",
            Location_X = 8,
            Location_Y = 8
        };
        List<int> ids = new List<int>() { s1.ID };
        
        Product p1 = new Product()
        {
            Brand = "",
            EAN = 1,
            ImageUrl = "",
            Measurement = "g",
            Name = "p1",
            Organic = false,
            Units = 100
        };
        Product p2 = new Product()
        {
            Brand = "",
            EAN = 2,
            ImageUrl = "",
            Measurement = "g",
            Name = "p2",
            Organic = false,
            Units = 100
        };
        
        ProductStore ps1 = new ProductStore()
        {
            StoreKey = s1.ID,
            ProductKey = p1.EAN,
            Price = 1
        };
        ProductStore ps2 = new ProductStore()
        {
            StoreKey = s2.ID,
            ProductKey = p2.EAN,
            Price = 1
        };

        context.Stores.Add(s1);
        context.Stores.Add(s2);
        context.Products.Add(p1);
        context.Products.Add(p2);
        context.ProductStores.Add(ps1);
        context.ProductStores.Add(ps2);
        context.SaveChanges();

        Assert.IsTrue(uut.GetProductsFromSpecificStores(ids, "p1", "g", false).Contains(p1));
        Assert.IsFalse(uut.GetProductsFromSpecificStores(ids, "p2", "g",false).Contains(p2));
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
}