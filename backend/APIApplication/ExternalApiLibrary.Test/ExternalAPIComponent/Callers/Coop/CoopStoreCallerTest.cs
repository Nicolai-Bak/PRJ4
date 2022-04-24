using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test;

public class CoopStoreCallerTest
{
	private CoopStoreCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void Setup()
	{
		_uut = new CoopStoreCaller();
	}

	[Test]
	public void Call_ValidRequest_ReturnsValidResponse()
	{
		_fakeRequest = Substitute.For<IRequest>();
		
		var responses = _uut.Call(_fakeRequest);
		
		Assert.That(responses.Result.Count, Is.EqualTo(CoopStoreCaller.StoresToRetrieve.Count));
	}
}
