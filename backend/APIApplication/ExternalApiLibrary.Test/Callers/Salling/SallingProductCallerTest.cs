using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalApiLibrary.Callers.Interfaces;
using ExternalApiLibrary.Callers.Salling;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingProductCallerTest
{
	private SallingProductCaller _uut;
	private IRequest _fakeRequest;

	private string _fakeProduct = @"
{
  'storeData': {
    '1373': {
      'inStock': true,
      'multipromo': '',
      'offerDescription': 'Tilbud',
      'price': 1200,
      'multiPromoPrice': '',
      'unitsOfMeasurePrice': 1595,
      'unitsOfMeasurePriceUnit': 'L.',
      'unitsOfMeasureOfferPrice': 1200,
      'unitsOfMeasureShowPrice': 1200
    }
  },
  'units': 1000,
  'unitsOfMeasure': 'ml',
  'images': [
    'https://dsdam.imgix.net/services/assets.img/id/97b8ad76-95fd-40fa-8fd4-002b30f4c330/size/DEFAULT.jpg'
  ],
  'infos': [
    {
      'code': 'description',
      'title': 'Beskrivelse',
      'items': [
        {
          'type': 1,
          'title': '',
          'value': 'Milkshakeblanding'
        }
      ]
    },
    {
      'code': 'ingredients',
      'title': 'Ingredienser',
      'items': [
        {
          'type': 1,
          'title': '',
          'value': 'MÆLK 4% (85%), sukker (12%), SKUMMETMÆLKSPULVER, emulgator (mono- og diglycerider af fedtsyrer af vegetabilsk oprindelse), sabilisator (carageenan, natriumalginat).'
        }
      ]
    },
    {
      'code': 'nutritional_100',
      'title': 'Næringsindhold pr. 100 gr.',
      'items': [
        {
          'type': 2,
          'title': 'Næringsindhold pr.',
          'value': '100 g'
        },
        {
          'type': 2,
          'title': 'Energi',
          'value': '507 kJ'
        },
        {
          'type': 2,
          'title': 'Energi',
          'value': '120 kcal'
        },
        {
          'type': 2,
          'title': 'Fedt',
          'value': '3,9 g'
        },
        {
          'type': 2,
          'title': '- heraf mættede fedtsyrer',
          'value': '2,6 g'
        },
        {
          'type': 2,
          'title': 'Kulhydrater',
          'value': '18 g'
        },
        {
          'type': 2,
          'title': '- heraf sukkerarter',
          'value': '18 g'
        },
        {
          'type': 2,
          'title': 'Protein',
          'value': '3,6 g'
        },
        {
          'type': 2,
          'title': 'Salt',
          'value': '0,10 g'
        }
      ]
    },
    {
      'code': 'product_details',
      'title': 'Produkt detaljer',
      'items': [
        {
          'type': 2,
          'title': 'Netto mængde',
          'value': '1.000 ml'
        },
        {
          'type': 2,
          'title': 'Produkt type',
          'value': 'Blanding til mælkedrik'
        },
        {
          'type': 2,
          'title': 'EAN',
          'value': '5711953153785'
        },
        {
          'type': 2,
          'title': 'Artikel',
          'value': '10181834'
        },
        {
          'type': 2,
          'title': 'PID',
          'value': '91794'
        }
      ]
    },
    {
      'code': 'productspecification',
      'title': 'Specificationer',
      'items': [
        {
          'type': 2,
          'title': 'Maksimal temperatur opbevaring',
          'value': '5 c'
        },
        {
          'type': 2,
          'title': 'Minimumstemperatur opbevaring',
          'value': '2 c'
        }
      ]
    }
  ],
  'objectID': '91794',
  '_highlightResult': {
    'searchHierachy': [
      {
        'value': 'Milkshake Mix',
        'matchLevel': 'none',
        'matchedWords': []
      },
      {
        'value': 'Milkshake',
        'matchLevel': 'none',
        'matchedWords': []
      },
      {
        'value': 'Kolde kaffedrikke og kakao',
        'matchLevel': 'none',
        'matchedWords': []
      },
      {
        'value': 'Mejeri',
        'matchLevel': 'none',
        'matchedWords': []
      },
      {
        'value': 'Mejeri & køl',
        'matchLevel': 'none',
        'matchedWords': []
      }
    ],
    'name': {
      'value': 'Milkshakeblanding',
      'matchLevel': 'none',
      'matchedWords': []
    },
    'manufacturer': {
      'value': '',
      'matchLevel': 'none',
      'matchedWords': []
    },
    'brand': {
      'value': 'Matilde',
      'matchLevel': 'none',
      'matchedWords': []
    },
    'subBrand': {
      'value': '',
      'matchLevel': 'none',
      'matchedWords': []
    },
    'productName': {
      'value': 'Milkshake Mix',
      'matchLevel': 'none',
      'matchedWords': []
    }
  }
}";
	
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
	
	[Test]
	public async Task Call_NonEmptyRequest_RequestCallAllReceived()
	{
		_fakeRequest.CallAll().Returns(new List<object> { new List<JObject> {JObject.Parse(_fakeProduct)} });
		await _uut.Call();
		await _fakeRequest.Received(1).CallAll();
	}
}
