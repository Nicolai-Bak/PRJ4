using ExternalApiLibrary.ExternalAPIComponent.Callers.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;
using ExternalApiLibrary.ExternalAPIComponent.Factory;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Factories;

[TestFixture]
public class CoopStoreFactoryTest
{
    private IApiFactory _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new CoopStoreFactory();
    }

    [Test]
    public void CreateCallerTest()
    {
        var returnValue = _uut.CreateCaller();
        Assert.AreEqual( returnValue.GetType(), typeof(CoopStoreCaller));
    }
    [Test]
    public void CreateFilterTest()
    {
        var returnValue = _uut.CreateFilter();
        Assert.AreEqual( returnValue.GetType(), typeof(CoopStoreFilter));
    }
    [Test]
    public void CreateConverterTest()
    {
        var returnValue = _uut.CreateConverter();
        Assert.AreEqual( returnValue.GetType(), typeof(CoopStoreConverter));
    }
}