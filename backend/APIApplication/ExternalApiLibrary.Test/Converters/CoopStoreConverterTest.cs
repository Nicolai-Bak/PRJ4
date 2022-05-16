using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Coop;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

[TestFixture]
public class CoopStoreConverterTest
{
	private IConverter _uut;

	[SetUp]
	public void Setup()
	{
		_uut = new CoopStoreConverter();
	}
	
	[Test]
	public void ConvertStoreTest()
	{
		var testStore = CreateTestStore();
		var controlStore = CreateComparisonStore();
	    
		var convertedStore = _uut.Convert(new List<IFilteredDto> { testStore });
	    
		Assert.That(convertedStore.Count, Is.EqualTo(1));

		Assert.That(convertedStore[0],
			Has.Property("ID").EqualTo(1)
			& Has.Property("Address").EqualTo(controlStore.Address)
			& Has.Property("Brand").EqualTo(controlStore.Brand)
			& Has.Property("Location_X").EqualTo(controlStore.Location_X)
			& Has.Property("Location_Y").EqualTo(controlStore.Location_Y));
	}
	
	private static FilteredCoopStore CreateTestStore()
	{
		return new FilteredCoopStore
		{
			Kardex = 1110,
			RetailGroupName = "Kvickly",
			Location = new List<double> { 12.6040707d, 55.6567268d },
			Address = "Englandsvej 28",
			City = "København S",
			Name = "Kvickly Sundby",
			Zipcode = 2300
		};
	}

	private static Store CreateComparisonStore()
	{
		return new Store
		{
			Address = "Englandsvej 28, 2300 København S",
			Brand = "Kvickly",
			ID = 1,
			Location_X = 12.6040707d,
			Location_Y = 55.6567268d
		};
	}
}
