using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Utilities.CredentialManager;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.ExternalAPIComponent.Callers.Salling;

public class SallingRequestBuilderTest
{
	private SallingRequestBuilder _uut;
	
	[SetUp]
	public void Setup()
	{
		_uut = new SallingRequestBuilder();
	}
	
	[Test]
	public void Ctor_Default_ReturnsInstance()
	{
		Assert.That(_uut, Is.Not.Null);
	}

	[Test]
	public void Build_NoParameters_ReturnNoParameterRequest()
	{
		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.Zero);
	}
	
	[Test]
	public void Build_OneParameter_ReturnOneParameterRequest()
	{
		_uut.AddName();
		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.EqualTo(1));
		Assert.That(request.Parameters[0], Is.EqualTo("name"));
	}
	
	[Test]
	public void Build_MultipleParameters_ReturnMultiParameterRequest()
	{
		_uut.AddName();
		_uut.AddImages();
		
		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.EqualTo(2));
		Assert.That(request.Parameters[0], Is.EqualTo("name"));
		Assert.That(request.Parameters[1], Is.EqualTo("images"));
	}
}
