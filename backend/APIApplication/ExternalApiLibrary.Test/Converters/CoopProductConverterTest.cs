using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

[TestFixture]
public class CoopProductConverterTest
{
	private IConverter _uut;

	[SetUp]
	public void Setup()
	{
		_uut = new CoopProductConverter();
	}
	
	[TestCase(true)]
	[TestCase(false)]
	public void ConvertTest(bool isOrganic)
	{
		var testProduct = CreateCoopTestProduct(isOrganic);
		var controlProduct = CreateComparisonObject(isOrganic);
	    
		var convertedProduct = _uut.Convert(new List<IFilteredDto> { testProduct });
	    
		Assert.That(convertedProduct.Count, Is.EqualTo(1));

		Assert.That(convertedProduct[0],
			Has.Property("EAN").EqualTo(controlProduct.EAN)
			& Has.Property("Name").EqualTo(controlProduct.Name)
			& Has.Property("Brand").EqualTo(controlProduct.Brand)
			& Has.Property("Units").EqualTo(controlProduct.Units).Within(0.09)
			& Has.Property("Measurement").EqualTo(controlProduct.Measurement)
			& Has.Property("Organic").EqualTo(controlProduct.Organic)
			& Has.Property("ImageUrl").EqualTo(controlProduct.ImageUrl));
	}

	private static FilteredCoopProduct CreateCoopTestProduct(bool isOrganic)
	{
		return new FilteredCoopProduct
		{
			id = "5700382823611",
			brand = "Änglamark",
			displayName = isOrganic ? "Økologiske Bananer" : "Bananer",
			category = "Frugt & grønt",
			image = "https://coopmad-website-prod-endpoint.azureedge.net/products/5700382823611.png?e=0x8D621EE56382034",
			pricePerUnitText = "24,62 kr. pr. kg",
			spotText = "Änglamark, 0,65 kg, Den Dominikanske republik, Kl. 1, ca 4-7 stk. pr. bundt",
			labels = new List<FilteredCoopProduct.Label>
			{
				new()
				{
					displayName = isOrganic ? "EU's Økologimærke" : "",
					id = isOrganic ? "eu-okologi" : "",
					parentId = isOrganic ? "økologi" : "",
					priority = 6
				},
				new()
				{
					displayName = "Nøglehulsmærket",
					id = "nøglehulsmærket",
					parentId = null,
					priority = 7
				},
				new()
				{
					displayName = "Fairtrade",
					id = "fairtrade",
					parentId = null,
					priority = 9
				}
			},
			salesPrice = new FilteredCoopProduct.SalesPrice
			{
				amount = 16d,
				formattedAmount = "16,-",
				formattedAmountLong = "16,00",
				major = "16",
				minor = "00",
				separator = ","
			}
		};
	}

	private static Product CreateComparisonObject(bool isOrganic)
	{
		return new Product
		{
			EAN = 5700382823611,
			Name = isOrganic ? "Økologiske Bananer" : "Bananer",
			Brand = "Änglamark",
			Units = 0.65,
			Measurement = "kg",
			Organic = isOrganic,
			ImageUrl = "https://coopmad-website-prod-endpoint.azureedge.net/products/5700382823611.png?e=0x8D621EE56382034",
			ProductStores = new List<ProductStore>
			{
				new()
				{
					Price = (long) 16d
				}
			}
		};
	}


}
