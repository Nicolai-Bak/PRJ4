using Algolia.Search.Clients;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Utilities.CredentialManager;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;

/**
 * Builds a request from a builder pattern of parameters
 */
public class SallingRequestBuilder : IRequestBuilder
{
    private readonly Request _newRequest;
    private readonly List<string> _parameters = new();

    public SallingRequestBuilder()
    {
        var credentials = Credentials.Instance;

        var client = new SearchClient(
            credentials.Keys["Salling-Application"],
            credentials.Keys["Salling-Admin"]);

        var index = client.InitIndex(credentials.Keys["Salling-IndexName"]);

        _newRequest = new Request(index);
    }

    public IRequest Build()
    {
        _newRequest.Parameters = _parameters;
        return _newRequest;
    }

    public IRequestBuilder AddImages()
    {
        _parameters.Add("images");
        return this;
    }

    public IRequestBuilder AddImageGuids()
    {
        _parameters.Add("imageGUIDs");
        return this;
    }

    public IRequestBuilder AddProductType()
    {
        _parameters.Add("productType");
        return this;
    }

    public IRequestBuilder AddStoreData()
    {
        _parameters.Add("storeData");
        return this;
    }

    public IRequestBuilder AddDescription()
    {
        _parameters.Add("description");
        return this;
    }

    public IRequestBuilder AddInfos()
    {
        _parameters.Add("infos");
        return this;
    }

    public IRequestBuilder AddUnits()
    {
        _parameters.Add("units");
        return this;
    }

    public IRequestBuilder AddUnitsOfMeasure()
    {
        _parameters.Add("unitsOfMeasure");
        return this;
    }

    public IRequestBuilder AddCountryOfOrigin()
    {
        _parameters.Add("countryOfOrigin");
        return this;
    }

    public IRequestBuilder AddProperties()
    {
        _parameters.Add("properties");
        return this;
    }

    public IRequestBuilder AddName()
    {
        _parameters.Add("name");
        return this;
    }

    public IRequestBuilder AddProductName()
    {
        _parameters.Add("productName");
        return this;
    }

    public IRequestBuilder AddArticle()
    {
        _parameters.Add("article");
        return this;
    }

    public IRequestBuilder AddBrand()
    {
        _parameters.Add("brand");
        return this;
    }

    public IRequestBuilder AddSubBrand()
    {
        _parameters.Add("subBrand");
        return this;
    }

    public IRequestBuilder AddObjectId()
    {
        _parameters.Add("objectId");
        return this;
    }

    public IRequestBuilder AddIsInOffer()
    {
        _parameters.Add("isInOffer");
        return this;
    }

    public IRequestBuilder AddCpOffer()
    {
        _parameters.Add("cpOffer");
        return this;
    }

    public IRequestBuilder AddCpOfferPrice()
    {
        _parameters.Add("cpOfferPrice");
        return this;
    }

    public IRequestBuilder AddCpOfferAmount()
    {
        _parameters.Add("cpOfferAmount");
        return this;
    }

    public IRequestBuilder AddCpOfferId()
    {
        _parameters.Add("cpOfferId");
        return this;
    }

    public IRequestBuilder AddCpOfferFromDate()
    {
        _parameters.Add("cpOfferFromDate");
        return this;
    }

    public IRequestBuilder AddCpOfferToDate()
    {
        _parameters.Add("cpOfferToDate");
        return this;
    }

    public IRequestBuilder AddCpOfferTitle()
    {
        _parameters.Add("cpOfferTitle");
        return this;
    }

    public IRequestBuilder AddCpDiscount()
    {
        _parameters.Add("cpDiscount");
        return this;
    }

    public IRequestBuilder AddCpPercentDiscount()
    {
        _parameters.Add("cpPercentDiscount");
        return this;
    }

    public IRequestBuilder AddCpOriginalPrice()
    {
        _parameters.Add("cpOriginalPrice");
        return this;
    }

    public IRequestBuilder AddConsumerFacingHierarchy()
    {
        _parameters.Add("consumerFacingHierarchy");
        return this;
    }

    public IRequestBuilder AddCategories()
    {
        _parameters.Add("categories");
        return this;
    }

    public IRequestBuilder AddSearchHierarchy()
    {
        _parameters.Add("searchHierchy");
        return this;
        // Typo in the Salling API
    }

    public IRequestBuilder AddPant()
    {
        _parameters.Add("pant");
        return this;
    }

    public IRequestBuilder AddInStockStore()
    {
        _parameters.Add("inStockStore");
        return this;
    }

    public IRequestBuilder AddOutStockStore()
    {
        _parameters.Add("outStockStore");
        return this;
    }

    public IRequestBuilder AddAttributes()
    {
        _parameters.Add("attributes");
        return this;
    }

    public IRequestBuilder AddDeepestCategoryPath()
    {
        _parameters.Add("deepestCategoryPath");
        return this;
    }
}