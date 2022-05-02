using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Coop;

public class CoopStoreCallerTest
{
	private CoopStoreCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void Setup()
	{
		_uut = new CoopStoreCaller(_fakeRequest);
	}

	[Test]
	public void Call_ValidRequest_ReturnsValidResponse()
	{
		_fakeRequest = Substitute.For<IRequest>();
		
		var responses = _uut.Call();
		
		Assert.That(responses.Result.Count, Is.GreaterThan(1));
	}
}
