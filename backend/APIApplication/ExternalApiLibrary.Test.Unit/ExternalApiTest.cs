using ExternalApiLibrary.ExternalAPIComponent;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit;

[TestFixture]
public class ExternalApiTest
{
    private IExternalApi _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new ExternalApi(null);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}