using ExternalApiLibrary.Callers.Salling;
using NUnit.Framework;

namespace ExternalApiLibrary.Test.Unit.Callers.Salling;

public class SallingRequestBuilderTest
{
	private SallingRequestBuilder _uut;
	
	[SetUp]
	public void Setup()
	{
		_uut = new SallingRequestBuilder();
	}
	
	[Test]
	public void Ctor_Default_ReturnsInstance()
	{
		Assert.That(_uut, Is.Not.Null);
	}

	[Test]
	public void Build_NoParameters_ReturnNoParameterRequest()
	{
		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.Zero);
	}
	
	[Test]
	public void Build_OneParameter_ReturnOneParameterRequest()
	{
		_uut.AddName();
		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.EqualTo(1));
		Assert.That(request.Parameters[0], Is.EqualTo("name"));
	}
	
	[Test]
	public void Build_AllParameters_ReturnAllParameterRequest()
	{
		_uut.AddImages()
			.AddImageGuids()
			.AddProductType()
			.AddArticle()
			.AddAttributes()
			.AddBrand()
			.AddCategories()
			.AddDescription()
			.AddInfos()
			.AddName()
			.AddPant()
			.AddProperties()
			.AddUnits()
			.AddCpDiscount()
			.AddCpOffer()
			.AddObjectId()
			.AddProductName()
			.AddSearchHierarchy()
			.AddStoreData()
			.AddSubBrand()
			.AddConsumerFacingHierarchy()
			.AddCountryOfOrigin()
			.AddCpOfferAmount()
			.AddCpOfferId()
			.AddCpOfferPrice()
			.AddCpOfferTitle()
			.AddCpOriginalPrice()
			.AddCpPercentDiscount()
			.AddDeepestCategoryPath()
			.AddInStockStore()
			.AddIsInOffer()
			.AddOutStockStore()
			.AddUnitsOfMeasure()
			.AddCpOfferFromDate()
			.AddCpOfferToDate();

		var request = (SallingRequest) _uut.Build();
		
		Assert.That(request, Is.Not.Null);
		Assert.That(request.Parameters.Count, Is.EqualTo(35));
	}
}
