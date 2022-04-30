using ExternalApiLibrary.Callers.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingStoreCallerTest
{
	private SallingStoreCaller _uut;
	
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
}
