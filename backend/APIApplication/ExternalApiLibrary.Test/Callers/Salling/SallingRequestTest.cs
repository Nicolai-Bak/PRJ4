using Algolia.Search.Clients;
using ExternalApiLibrary.Callers.Salling;
using ExternalApiLibrary.Utilities.CredentialManager;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingRequestTest
{
	private SallingRequest _uut;

	[SetUp]
	public void Setup()
	{
		var credentials = Credentials.Instance;

		var client = new SearchClient(
			credentials.Keys["Salling-Application"],
			credentials.Keys["Salling-Admin"]);

		var index = client.InitIndex(credentials.Keys["Salling-IndexName"]);
		
		_uut = new SallingRequest(index);
	}
	
	[Test]
	public void CallPage_NoParameters_60Hits()
	{
		var result = _uut.CallPage();
		
		Assert.That(result.Result.Count, Is.EqualTo(60));
	}
	
	[Test]
	public void CallPage_WithParameters_60Hits()
	{
		_uut.Parameters.Add("units");
		
		var result = _uut.CallPage();
		
		Assert.That(result.Result.Count, Is.EqualTo(60));
	}
	
	[Test]
	public void CallAll_NoParameters_2Hits()
	{
		var result = _uut.CallAll();
		
		Assert.That(result.Result.Count, Is.EqualTo(2));
	}
	
	[Test]
	public void CallAll_WithParameters_2Hits()
	{
		_uut.Parameters.Add("units");

		var result = _uut.CallAll();
		
		Assert.That(result.Result.Count, Is.EqualTo(2));
	}
}
