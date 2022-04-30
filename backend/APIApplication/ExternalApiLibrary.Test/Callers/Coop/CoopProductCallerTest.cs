using System.Threading.Tasks;
using ExternalApiLibrary.Callers.Coop;
using ExternalApiLibrary.Callers.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Coop;

public class CoopProductCallerTest
{
	private CoopProductCaller _uut;
	private IRequest _fakeRequest;
	
	[SetUp]
	public void SetupCoopProductCaller()
	{
		_fakeRequest = Substitute.For<IRequest>();

		_uut = new CoopProductCaller(_fakeRequest);
	}

	[Test]
	public async Task Call_EmptyRequest_RequestCallAllReceived()
	{
		await _uut.Call();
		await _fakeRequest.Received(1).CallAll();
	}
}
