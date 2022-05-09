using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Converters.Salling;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

[TestFixture]
public class SallingStoreConverterTest
{
    private IConverter _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new SallingStoreConverter();
    }

    [Test]
    public void ConvertStoreTest()
    {
	    var testStore = CreateSallingTestStore();
	    var controlStore = CreateComparisonStore();
	    
	    var convertedStore = _uut.Convert(new List<IFilteredDto> { testStore });
	    
	    Assert.That(convertedStore.Count, Is.EqualTo(1));

	    Assert.That(convertedStore[0],
		    Has.Property("ID").EqualTo(controlStore.ID)
		    & Has.Property("Address").EqualTo(controlStore.Address)
		    & Has.Property("Brand").EqualTo(controlStore.Brand)
		    & Has.Property("Location_X").EqualTo(controlStore.Location_X)
		    & Has.Property("Location_Y").EqualTo(controlStore.Location_Y));
    }

    /**
     * This utility method creates a test sample of a received store.
     */
    private static FilteredSallingStore CreateSallingTestStore()
    {
	    return new FilteredSallingStore
	    {
		    Id = 1652,
		    Brand = "foetex",
		    Coordinates = new List<double> { 9.876099d, 57.004687d },
		    AddressField = new FilteredSallingStore.Address
		    {
			    City = "Aalborg Sv",
			    Country = "DK",
			    Street = "Hobrovej 450",
			    Zip = "9200"
		    }
	    };
    }

    private static Store CreateComparisonStore()
    {
	    return new Store
	    {
			Address = "Hobrovej 450, 9200 Aalborg Sv, DK",
			Brand = "foetex",
			ID = 1652,
			Location_X = 9.876099d,
			Location_Y = 57.004687d
	    };
    }
}