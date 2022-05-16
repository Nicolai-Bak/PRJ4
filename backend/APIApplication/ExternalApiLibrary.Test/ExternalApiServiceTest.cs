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
        await _uut.UpdateDatabase();

        _dbInsert.Received(1).ClearDatabase();
        _dbInsert.Received(1).InsertProducts(Arg.Any<List<Product>>());
        _dbInsert.Received(1).InsertStores(Arg.Any<List<Store>>());
        _dbInsert.Received(1).InsertProductStores(Arg.Any<List<ProductStore>>());
        _standardizer.Received(1).Standardize(Arg.Any<List<Product>>());
        _dbInsert.Received(1).InsertProductStandardNames(Arg.Any<List<ProductStandardName>>());
    }
}