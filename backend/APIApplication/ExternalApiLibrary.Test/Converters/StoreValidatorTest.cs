using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

public class StoreValidatorTest
{
	[SetUp]
	public void Setup()
	{
		// Static class, no setup required
	}

	[Test]
	public void ValidateStores_ValidStore_ReturnWholeList()
	{
		// Arrange
		var store = new Store
		{
			ID = 1,
			Brand = "Test",
			Location_X = 1.0,
			Location_Y = 1.0,
			Address = "Test",
			ProductStores = new List<ProductStore> { new() }
		};

		var stores = new List<Store>
		{
			store
		};

		// Act
		var result = StoreValidator.ValidateStores(stores, StoreGroup.Coop);

		// Assert
		Assert.AreEqual(stores, result);
	}
	
	[Test]
	public void ValidateStores_InvalidStore_ReturnEmptyList()
	{
		// Arrange
		var store = new Store
		{
			ID = -1,
			Location_X = -1.0,
			Location_Y = -1.0,
			ProductStores = new List<ProductStore>()
		};

		var stores = new List<Store>
		{
			store
		};

		// Act
		var result = StoreValidator.ValidateStores(stores, StoreGroup.Coop);


		// Assert
		Assert.That(result.Count, Is.EqualTo(0));
	}
}
