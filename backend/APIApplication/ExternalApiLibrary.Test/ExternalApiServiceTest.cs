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
    private Dictionary<IApiFactory,IApiFactory> _factories;
    private IProductNameStandardizer _standardizer;

    [SetUp]
    public void Setup()
    {
        _dbInsert = Substitute.For<IDbInsert>();
        _factories = new Dictionary<IApiFactory,IApiFactory>()
        {
            {
                Substitute.For<IApiFactory>(),
                Substitute.For<IApiFactory>()
            },
            {
                Substitute.For<IApiFactory>(),
                Substitute.For<IApiFactory>()
            }
        };
        _standardizer = Substitute.For<IProductNameStandardizer>();

        _uut = new ExternalApiService(_dbInsert,
                                    _factories,
                                    _standardizer,
                                    false);
    }

    [Test]
    public async Task UpdateDatabaseTest()
    {
        List<IDbModelsDto> products0 = new List<IDbModelsDto>(){Substitute.For<IDbModelsDto>()};
        List<IDbModelsDto> stores0 = new List<IDbModelsDto>(){new Store()};
        List<IDbModelsDto> products1 = new List<IDbModelsDto>(){new Product()};
        List<IDbModelsDto> stores1 = new List<IDbModelsDto>(){new Store()};
        List<ProductStore> productsStore = new List<ProductStore>();
        List<ProductStandardName> productsStandardName = new List<ProductStandardName>();

        _uut.ExternalApis.Keys.ElementAt(0).Get().Returns(Task.FromResult(products0));
        _uut.ExternalApis.Values.ElementAt(0).Get().Returns(stores0);
        _uut.ExternalApis.Keys.ElementAt(1).Get().Returns(products1);
        _uut.ExternalApis.Values.ElementAt(1).Get().Returns(stores1);
        
        products0.AddRange(products1);
        stores0.AddRange(stores1);

        List<Product> returnProducts = products0.Cast<Product>().ToList();

        _dbInsert.GetAllProducts().Returns(returnProducts);
        _standardizer.Standardize(returnProducts).ReturnsForAnyArgs(productsStandardName);
        
        
        await _uut.UpdateDatabase();

        _dbInsert.Received(1).ClearDatabase();
        _dbInsert.Received(1).InsertProducts(products0.Cast<Product>().ToList());
        _dbInsert.Received(1).InsertStores(stores0.Cast<Store>().ToList());
        _dbInsert.Received(1).InsertProductStores(productsStore);
        _standardizer.Received(1).Standardize(products0.Cast<Product>().ToList());
        _dbInsert.Received(1).InsertProductStandardNames(productsStandardName);
    }
}