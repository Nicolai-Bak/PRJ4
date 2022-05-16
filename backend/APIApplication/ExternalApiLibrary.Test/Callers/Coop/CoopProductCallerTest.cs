using System.Collections.Generic;
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

	private const string FakeProduct = @"{'products': [
		{
            'id': '5700384500909',
            'displayName': 'Økologisk Hummus',
            'category': 'Kolonial',
            'image': 'https://coopmad-website-prod-endpoint.azureedge.net/products/5700384500909.png?e=0x8D6F8A7B4872E81',
            'url': '/kolonial/dressing-pesto-mm/hummus/irmas-oekologisk-hummus-p-5700384500909',
            'brand': 'Irmas',
            'alternativeUrls': [
                '/nem-mad/irmas-oekologisk-hummus-p-5700384500909'
            ],
            'splash': {
                'splashType': 8,
                'name': 'MixOffer'
            },
            'discountLabel': {
                'minQuantity': 2,
                'price': {
                    'separator': ',',
                    'amount': 44.0,
                    'formattedAmount': '44,-',
                    'formattedAmountLong': '44,00',
                    'minor': '00',
                    'major': '44'
                },
                'saved': {
                    'separator': ',',
                    'amount': 9.90,
                    'formattedAmount': '9,90',
                    'formattedAmountLong': '9,90',
                    'minor': '90',
                    'major': '9'
                },
                'savedPerItem': {
                    'separator': ',',
                    'amount': 4.95,
                    'formattedAmount': '4,95',
                    'formattedAmountLong': '4,95',
                    'minor': '95',
                    'major': '4'
                },
                'isMix': true,
                'savedPercentage': null,
                'hasNewSalesPrice': false,
                'discountLabelType': 3,
                'showSavings': false,
                'name': 'BuyXForYSaveZDiscount',
                'usageLimitPerOrder': null,
                'defaultQuantity': null
            },
            'reviews': null,
            'productType': 0,
            'spotText': 'Irmas, 0,175 kg',
            'labels': [
                {
                    'id': 'eu-okologi',
                    'parentId': 'okologi',
                    'displayName': 'EU\'s Økologimærke',
                    'priority': 6
                }
            ],
            'salesPrice': {
                'separator': ',',
                'amount': 26.95,
                'formattedAmount': '26,95',
                'formattedAmountLong': '26,95',
                'minor': '95',
                'major': '26'
            },
            'bundleProducts': null,
            'originalPrice': null,
            'pricePerUnitText': '154,- kr. pr. kg',
            'available': true,
            'maxQuantity': 999,
            'defaultQuantity': 1,
            'defaultQuantityPriceTotal': null,
            'defaultQuantityPricePerUnit': null,
            'onlyPurchaseableFromProductDetailsPage': false,
            'isWine': false,
            'wineOverview': null,
            'contentsText': null,
            'alcoholPercentage': 0.0,
            'showAgeRequirement': false,
            'isInAssortment': true,
            'isAvailableToAddInLimitedDeliveryPeriod': true,
            'outsideDeliveryPeriodMessage': null,
            'trackingContainer': null,
            'hasOffer': true,
            'isFavorited': false,
            'medicineGroup': null
        }]}";

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
	
	[Test]
	public async Task Call_NonEmptyRequest_RequestCallAllReceived()
	{
		_fakeRequest.CallAll().Returns(new List<object> { FakeProduct });
		await _uut.Call();
		await _fakeRequest.Received(1).CallAll();
	}
}
