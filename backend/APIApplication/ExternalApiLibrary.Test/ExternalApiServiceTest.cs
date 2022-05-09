using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLibrary.ProductNameStandardize;
using DatabaseLibrary;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.HostedServices;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit;

[TestFixture]
public class ExternalApiServiceTest
{
    private ExternalApiService _uut;
    private IDbInsert _dbInsert;
    private List<IApiFactory[]> _factories;
    private IProductNameStandardizer _standardizer;

    [SetUp]
    public void Setup()
    {
        _dbInsert = Substitute.For<IDbInsert>();
        _factories = new List<IApiFactory[]>()
        {
            new IApiFactory[2]
            {
                Substitute.For<IApiFactory>(),
                Substitute.For<IApiFactory>(),
            },
            new IApiFactory[2]
            {
                Substitute.For<IApiFactory>(),
                Substitute.For<IApiFactory>(),
            },
        };
        _standardizer = Substitute.For<IProductNameStandardizer>();

        _uut = new ExternalApiService(_dbInsert,
                                    _factories,
                                    _standardizer,
                                    false);
    }

    [Test]
    public async Task UpdateDasebaseTest()
    {
        List<IDbModelsDto> products0 = new List<IDbModelsDto>();
        List<IDbModelsDto> stores0 = new List<IDbModelsDto>();
        List<IDbModelsDto> products1 = new List<IDbModelsDto>();
        List<IDbModelsDto> stores1 = new List<IDbModelsDto>();
        List<ProductStore> productsStore = new List<ProductStore>();
        List<ProductStandardName> productsStandardName = new List<ProductStandardName>();

        _uut._externalApis[0][0].Get().Returns(products0);
        _uut._externalApis[0][1].Get().Returns(stores0);
        _uut._externalApis[1][0].Get().Returns(products1);
        _uut._externalApis[1][1].Get().Returns(stores1);
        
        products0.AddRange(products1);
        stores0.AddRange(stores1);

        _dbInsert.GetAllProducts().Returns(products0.Cast<Product>().ToList());
        _standardizer.Standardize(products0.Cast<Product>().ToList()).Returns(productsStandardName);
        
        
        await _uut.UpdateDatabase();

        _dbInsert.Received(1).ClearDatabase();
        _dbInsert.Received(1).InsertProducts(products0.Cast<Product>().ToList());
        _dbInsert.Received(1).InsertStores(stores0.Cast<Store>().ToList());
        _dbInsert.Received(1).InsertProductStores(productsStore);
        _standardizer.Received(1).Standardize(products0.Cast<Product>().ToList());
        _dbInsert.Received(1).InsertProductStandardNames(productsStandardName);
    }
}