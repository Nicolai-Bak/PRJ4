using ExternalAPIComponent.Callers.Coop;
using ExternalAPIComponent.Callers.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test;

public class Tests
{
	private CoopProductCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void SetupCoopProductCaller()
	{
		_uut = new CoopProductCaller();
	}

	[Test]
	public void Call_EmptyRequest_RequestCallAllReceived()
	{
		_fakeRequest = Substitute.For<IRequest>();

		_uut.Call(_fakeRequest);
		_fakeRequest.Received(1).CallAll();
	}
}
