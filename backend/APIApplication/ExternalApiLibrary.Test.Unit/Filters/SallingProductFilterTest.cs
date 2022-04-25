using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Filters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Filters;
[TestFixture]
public class SallingProductFilterTest
{
    private IFilter _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new SallingProductFilter();
    }

    [Test]
    public void Test1()
    {

        Assert.Pass();
    }
}