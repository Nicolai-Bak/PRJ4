using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.ExternalAPIComponent.Callers.Salling;

public class SallingProductCallerTest
{
	private SallingProductCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void SetupCoopProductCaller()
	{
		_uut = new SallingProductCaller();
	}

	[Test]
	public void Call_EmptyRequest_RequestCallAllReceived()
	{
		_fakeRequest = Substitute.For<IRequest>();

		_uut.Call(_fakeRequest);
		_fakeRequest.Received(1).CallAll();
	}
}
