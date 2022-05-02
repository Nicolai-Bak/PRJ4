using System.Threading.Tasks;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingProductCallerTest
{
	private SallingProductCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void SetupCoopProductCaller()
	{
		_fakeRequest = Substitute.For<IRequest>();

		_uut = new SallingProductCaller(_fakeRequest);
	}

	[Test]
	public async Task Call_EmptyRequest_RequestCallAllReceived()
	{
		await _uut.Call();
		await _fakeRequest.Received(1).CallAll();
	}
}
