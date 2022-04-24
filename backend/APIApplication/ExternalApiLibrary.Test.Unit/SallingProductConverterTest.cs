using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit;

[TestFixture]
public class SallingProductConverterTest
{
    private IConverter _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new SallingProductConverter();
    }
    
    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}