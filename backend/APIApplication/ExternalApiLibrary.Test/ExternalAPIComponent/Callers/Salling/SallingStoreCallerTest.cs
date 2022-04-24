using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.ExternalAPIComponent.Callers.Salling;

public class SallingStoreCallerTest
{
	private SallingStoreCaller _uut;
	
	[SetUp]
	public void Setup()
	{
		_uut = new SallingStoreCaller();
	}

	[Test]
	public void Call_ValidRequest_ReturnsValidResponse()
	{
		var responses = _uut.Call(null).Result;
		
		Assert.That(responses, Is.Not.Null);
		Assert.That(responses.Count, Is.GreaterThan(0));
	}
}
