using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Converters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Factories;

[TestFixture]
public class FøtexProductFactoryTest
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