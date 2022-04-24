using ExternalAPIComponent.Callers.Coop;
using NUnit.Framework;

namespace ExternalApiLibrary.Test;

public class CoopRequestBuilderTest
{
	private CoopRequestBuilder _uut;
	
	[SetUp]
	public void Setup()
	{
		_uut = new CoopRequestBuilder();
	}
	
	[Test]
	public void BuildRequest_ReturnsCorrectRequest()
	{
		var request = (CoopRequest) _uut.Build();

		Assert.IsInstanceOf<CoopRequest>(request);
		Assert.That(request.BaseUrl, Is.EqualTo("https://mad.coop.dk/api/search/products"));
	}
}
