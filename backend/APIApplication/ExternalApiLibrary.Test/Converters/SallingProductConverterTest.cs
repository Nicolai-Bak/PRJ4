using System;
using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

[TestFixture]
public class SallingProductConverterTest
{
    private IConverter _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new SallingProductConverter();
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ConvertTest(bool isOrganic)
    {
	    var testProduct = CreateSallingTestProduct(isOrganic);
	    var controlProduct = CreateComparisonObject(isOrganic);
	    
	    var convertedProduct = _uut.Convert(new List<IFilteredDto> { testProduct });
	    
	    Assert.That(convertedProduct.Count, Is.GreaterThan(0));

	    Assert.That(convertedProduct[0],
		    Has.Property("EAN").EqualTo(controlProduct.EAN)
		    & Has.Property("Name").EqualTo(controlProduct.Name)
		    & Has.Property("Brand").EqualTo(controlProduct.Brand)
		    & Has.Property("Units").EqualTo(controlProduct.Units).Within(0.09)
		    & Has.Property("Measurement").EqualTo(controlProduct.Measurement)
		    & Has.Property("Organic").EqualTo(controlProduct.Organic)
		    & Has.Property("ImageUrl").EqualTo(controlProduct.ImageUrl));
    }

    private static FilteredSallingProduct CreateSallingTestProduct(bool isOrganic)
    {
	    var properties = new List<string>();
	    
	    if (isOrganic)
		    properties.Add("økologisk");
	    
	    return new FilteredSallingProduct
	    {
		    Units = 1.2f,
		    UnitsOfMeasure = "Kg.",
		    Image = new List<string> {"https://dsdam.imgix.net/services/assets.img/id/fdccb42e-9a69-44bf-b3b0-7bccd2fd11be/size/DEFAULT.jpg"},
		    Properties = properties,
		    Stores = new Dictionary<int, FilteredSallingProduct.StoreData>
		    {
			    {
				    1373, new FilteredSallingProduct.StoreData
				    {
					    Price = 4900
				    }
			    }
		    },
		    Infos = new List<FilteredSallingProduct.Info>
		    {
			    new()
			    {
				    Code = "product_details",
				    Title = "Produkt detaljer",
				    Items = new List<FilteredSallingProduct.Info.Item>
				    {
					    new()
					    {
						    Title = "EAN",
						    Value = "5707196297447"
					    }
				    }
			    }
		    },
		    HighlightResults = new FilteredSallingProduct.HighlightResult
		    {
			    ProductName = new FilteredSallingProduct.HighlightResult.Value { Text = (isOrganic) ? "Medister Øko" : "Medister" },
			    Brand = new FilteredSallingProduct.HighlightResult.Value { Text = "Slagteren" }
		    }
	    };
    }

    private static Product CreateComparisonObject(bool isOrganic)
    {
	    return new Product
	    {
		    EAN = 5707196297447,
		    Name = isOrganic ? "Medister Øko" : "Medister",
		    Brand = "Slagteren",
		    Units = 1.2d,
		    Measurement = "Kg.",
		    Organic = isOrganic,
		    ImageUrl = "https://dsdam.imgix.net/services/assets.img/id/fdccb42e-9a69-44bf-b3b0-7bccd2fd11be/size/DEFAULT.jpg",
		    ProductStores = new List<ProductStore>
		    {
			    new()
			    {
				    Price = 4900
			    }
		    }
	    };
    }
}