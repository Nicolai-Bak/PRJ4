using System.Collections.Generic;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Factory;
using ExternalApiLibrary.Filters.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit;

[TestFixture]
public class ExternalApiTest
{
    private IExternalApi _uut;
    private  IApiFactory _factory;
    private ICaller _caller;
    private IFilter _filter;
    private IConverter _converter;
    private IRequest _request;
    

    [SetUp]
    public void Setup()
    {
        _factory = Substitute.For<IApiFactory>();
        _caller = Substitute.For<ICaller>();
        _filter = Substitute.For<IFilter>();
        _converter = Substitute.For<IConverter>();
        _request = Substitute.For<IRequest>();
        _factory.CreateCaller().Returns(_caller);
        _factory.CreateFilter().Returns(_filter);
        _factory.CreateConverter().Returns(_converter);
        _uut = new ExternalApi(_factory);
    }

    [Test]
    public void ConstructorTest()
    {
        _factory.Received(1).CreateCaller();
        _factory.Received(1).CreateFilter();
        _factory.Received(1).CreateConverter();
    }

    [Test]
    public void GetTest()
    {
        List<object> list = new List<object>();
        List<object> filteredList = new List<object>();
        _caller.Call(_request).Returns(list);
        _filter.Filter(list).Returns(filteredList);

        _uut.Get(_request);
        _caller.Received(1).Call(_request);
        _filter.Received(1).Filter(list);
        _converter.Received(1).Convert(filteredList);
    }
}