using ExternalAPIComponent.Callers.Coop;
using NUnit.Framework;

namespace ExternalApiLibrary.Test;

public class CoopRequestTest
{
	private CoopRequest _uut;
	
	[SetUp]
	public void Setup()
	{
		_uut = new CoopRequest("https://mad.coop.dk/api/search/products", 1);
	}
	
	[Test]
	public void Ctor_ValidBaseUrl_BaseUrlSet()
	{
		_uut = new CoopRequest("https://www.test.dk");
		Assert.That(_uut.BaseUrl, Is.EqualTo("https://www.test.dk"));
	}
	
	[Test]
	public void Ctor_NullBaseUrl_ThrowsArgumentNullException()
	{
		Assert.That(() => new CoopRequest(null), Throws.ArgumentNullException);
	}
	
	[Test]
	public void CallAll_ValidBaseUrlWith1PageSize_10ValidResponsesReceived()
	{
		var responses = _uut.CallAll();

		responses.Result.ForEach(Assert.NotNull);
		Assert.That(responses.Result.Count, Is.EqualTo(10));
	}
	
	[Test]
	public void CallAll_InvalidBaseUrl_ExceptionThrown()
	{
		_uut = new CoopRequest("https://orjtthyh09rtyh.erhstreh/");
		
		Assert.That(() => _uut.CallAll(), Throws.Exception);
	}

	[Test]
	public void SwapToProduction_RetrieveAllIsFalse_RetrieveAllIsTrue()
	{
		_uut.SwapToProduction();
		Assert.That(_uut.RetrieveAll, Is.True);
	}
}
