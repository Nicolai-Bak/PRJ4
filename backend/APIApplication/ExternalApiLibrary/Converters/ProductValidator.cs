using DatabaseLibrary.Models;
using Serilog;

namespace ExternalApiLibrary.Converters;

public enum ProductGroup
{
	Salling,
	Coop
}

public static class ProductValidator
{
	
	
	public static List<IDbModelsDto> ValidateProducts(List<Product>? products, ProductGroup productGroup)
	{
		var validProducts = new List<IDbModelsDto>();

		products?.ForEach(p =>
		{
			bool isValid = true;

			if (p.EAN <= 0)
			{
				LogInvalidProduct($"Ean was less than or equal to 0", p, productGroup);
				isValid = false;
			}

			if (string.IsNullOrEmpty(p.Name) || string.IsNullOrWhiteSpace(p.Name))
			{
				LogInvalidProduct($"Name was empty", p, productGroup);
				isValid = false;
			}

			if (string.IsNullOrEmpty(p.Brand) || string.IsNullOrWhiteSpace(p.Brand))
			{
				LogInvalidProduct($"Brand was empty", p, productGroup);
				isValid = false;
			}

			if (p.Units <= 0)
			{
				LogInvalidProduct($"Units was less than or equal to zero", p, productGroup);
				isValid = false;
			}

			if (string.IsNullOrEmpty(p.Measurement) || string.IsNullOrWhiteSpace(p.Measurement))
			{
				LogInvalidProduct($"Measurement was empty", p, productGroup);
				isValid = false;
			}

			if (string.IsNullOrEmpty(p.ImageUrl) || string.IsNullOrWhiteSpace(p.ImageUrl))
			{
				LogInvalidProduct($"ImageUrl was empty", p, productGroup);
				isValid = false;
			}

			if (p.ProductStores.Count == 0)
			{
				LogInvalidProduct($"ProductStores was empty", p, productGroup);
				isValid = false;
			}

			if (isValid)
				validProducts.Add(p);
		});

		return validProducts;
	}

	private static void LogInvalidProduct(string msg, Product product, ProductGroup productGroup)
	{
		Log.Fatal($"[{Enum.GetName(typeof(ProductGroup), productGroup) + "ProductConverter"}] " +
		          $"Invalid product conversion error: {@msg}\nProduct: {@product}",
			msg, product);
	}
}
