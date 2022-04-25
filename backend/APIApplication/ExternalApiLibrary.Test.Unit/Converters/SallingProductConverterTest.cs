using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

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