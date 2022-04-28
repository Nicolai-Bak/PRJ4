using System.Collections.Generic;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Factory;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit;

[TestFixture]
public class ExternalApiTest
{
    private IExternalApi _uut;
    private  IApiFactory _factory;
    private ICaller _caller;
    private IConverter _converter;
    private IRequest _request;
    
    [SetUp]
    public void Setup()
    {
        _factory = Substitute.For<IApiFactory>();
        _caller = Substitute.For<ICaller>();
        _converter = Substitute.For<IConverter>();
        _request = Substitute.For<IRequest>();
        _factory.CreateCaller().Returns(_caller);
        _factory.CreateConverter().Returns(_converter);
        _uut = new ExternalApi(_factory);
    }

    [Test]
    public void ConstructorTest()
    {
        _factory.Received(1).CreateCaller();
        _factory.Received(1).CreateConverter();
    }

    [Test]
    public void GetTest()
    {
        var filteredList = new List<IFilteredDto>();
        _caller.Call().Returns(filteredList);

        _uut.Get();
        _caller.Received(1).Call();
        _converter.Received(1).Convert(filteredList);
    }
}