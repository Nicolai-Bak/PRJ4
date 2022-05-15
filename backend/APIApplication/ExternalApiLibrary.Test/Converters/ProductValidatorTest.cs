using System.Collections.Generic;
using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Converters;

public class ProductValidatorTest
{
	[SetUp]
	public void Setup()
	{
		// Static class, no setup required
	}

	[Test]
	public void ValidateProducts_ValidProduct_ReturnWholeList()
	{
		// Arrange
		var product = new Product
		{
			EAN = 1,
			Name = "Test",
			Brand = "Test",
			Units = 1.0,
			Measurement = "Test",
			Organic = true,
			ImageUrl = "Test",
			ProductStores = new List<ProductStore> { new() }
		};

		var products = new List<Product>
		{
			product
		};

		// Act
		var result = ProductValidator.ValidateProducts(products, ProductGroup.Coop);

		// Assert
		Assert.AreEqual(products, result);
	}
	
	[Test]
	public void ValidateProducts_InvalidProduct_ReturnEmptyList()
	{
		// Arrange
		var product = new Product
		{
			EAN = -1,
			Units = -1.0,
			ProductStores = new List<ProductStore>()
		};

		var products = new List<Product>
		{
			product
		};

		// Act
		var result = ProductValidator.ValidateProducts(products, ProductGroup.Coop);

		// Assert
		Assert.That(result.Count, Is.EqualTo(0));
	}
}
