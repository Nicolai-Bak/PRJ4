using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.Filters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Factories;

[TestFixture]
public class FøtexStoreFactoryTest
{
    private IApiFactory _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new FoetexStoreFactory();
    }

    [Test]
    public void CreateCallerTest()
    {
        var returnValue = _uut.CreateCaller();
        Assert.AreEqual( returnValue.GetType(), typeof(SallingStoreCaller));
    }
    [Test]
    public void CreateFilterTest()
    {
        var returnValue = _uut.CreateFilter();
        Assert.AreEqual( returnValue.GetType(), typeof(SallingStoreFilter));
    }
    [Test]
    public void CreateConverterTest()
    {
        var returnValue = _uut.CreateConverter();
        Assert.AreEqual( returnValue.GetType(), typeof(SallingStoreConverter));
    }
}