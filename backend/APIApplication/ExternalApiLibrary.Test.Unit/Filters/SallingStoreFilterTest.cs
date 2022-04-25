using ExternalApiLibrary.Filters.Interfaces;
using ExternalApiLibrary.Filters.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Filters;

[TestFixture]
public class SallingStoreFilterTest
{
    private IFilter _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new SallingStoreFilter();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}