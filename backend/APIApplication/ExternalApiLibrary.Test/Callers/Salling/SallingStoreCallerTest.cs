using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingStoreCallerTest
{
	private SallingStoreCaller _uut;
	private IRequest _fakeRequest;

	[SetUp]
	public void Setup()
	{
		_uut = new SallingStoreCaller(null);
	}

	[Test]
	public void Call_ValidRequest_ReturnsValidResponse()
	{
		var responses = _uut.Call().Result;
		
		Assert.That(responses, Is.Not.Null);
		Assert.That(responses.Count, Is.GreaterThan(0));
	}
	
	[Test]
	public void GetSetRequest_ValidRequest_ReturnsValidRequest()
	{
		_fakeRequest = Substitute.For<IRequest>();
		
		_uut.Request = _fakeRequest;
		var request = _uut.Request;
		
		Assert.That(request, Is.EqualTo(_fakeRequest));
	}
}
