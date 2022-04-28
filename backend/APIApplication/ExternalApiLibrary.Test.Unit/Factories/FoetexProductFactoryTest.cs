using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.Factory;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Factories;

[TestFixture]
public class FoetexProductFactoryTest
{
    private IApiFactory _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new FoetexProductFactory();
    }

    [Test]
    public void CreateCallerTest()
    {
        var returnValue = _uut.CreateCaller();
        Assert.AreEqual( returnValue.GetType(), typeof(SallingProductCaller));
    }
    [Test]
    public void CreateConverterTest()
    {
        var returnValue = _uut.CreateConverter();
        Assert.AreEqual( returnValue.GetType(), typeof(SallingProductConverter));
    }
}