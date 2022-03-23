using Algolia.Search.Clients;

namespace ExternalAPIComponent.Callers;

public class RequestBuilder : IRequestBuilder
{
    private readonly Request _newRequest;
    private readonly List<string> parameters = new();

    public RequestBuilder()
    {
        var credentials = Credentials.Instance;

        var client = new SearchClient(
            credentials.Keys["Salling-Application"],
            credentials.Keys["Salling-Admin"]);

        var index = client.InitIndex(credentials.Keys["Salling-IndexName"]);

        _newRequest = new Request(client, index);
    }

    public IRequest Build()
    {
        _newRequest.SetParameters(parameters);
        return _newRequest;
    }

    public void AddImages()
    {
        parameters.Add("images");
    }

    public void AddImageGuids()
    {
        parameters.Add("imageGUIDs");
    }

    public void AddProductType()
    {
        parameters.Add("productType");
    }

    public void AddStoreData()
    {
        parameters.Add("storeData");
    }

    public void AddDescription()
    {
        parameters.Add("description");
    }

    public void AddInfos()
    {
        parameters.Add("infos");
    }

    public void AddUnits()
    {
        parameters.Add("units");
    }

    public void AddUnitsOfMeasure()
    {
        parameters.Add("unitsOfMeasure");
    }

    public void AddCountryOfOrigin()
    {
        parameters.Add("countryOfOrigin");
    }

    public void AddProperties()
    {
        parameters.Add("properties");
    }

    public void AddName()
    {
        parameters.Add("name");
    }

    public void AddProductName()
    {
        parameters.Add("productName");
    }

    public void AddArticle()
    {
        parameters.Add("article");
    }

    public void AddBrand()
    {
        parameters.Add("brand");
    }

    public void AddSubBrand()
    {
        parameters.Add("subBrand");
    }

    public void AddObjectId()
    {
        parameters.Add("objectId");
    }

    public void AddIsInOffer()
    {
        parameters.Add("isInOffer");
    }

    public void AddCpOffer()
    {
        parameters.Add("cpOffer");
    }

    public void AddCpOfferPrice()
    {
        parameters.Add("cpOfferPrice");
    }

    public void AddCpOfferAmount()
    {
        parameters.Add("cpOfferAmount");
    }

    public void AddCpOfferId()
    {
        parameters.Add("cpOfferId");
    }

    public void AddCpOfferFromDate()
    {
        parameters.Add("cpOfferFromDate");
    }

    public void AddCpOfferToDate()
    {
        parameters.Add("cpOfferToDate");
    }

    public void AddCpOfferTitle()
    {
        parameters.Add("cpOfferTitle");
    }

    public void AddCpDiscount()
    {
        parameters.Add("cpDiscount");
    }

    public void AddCpPercentDiscount()
    {
        parameters.Add("cpPercentDiscount");
    }

    public void AddCpOriginalPrice()
    {
        parameters.Add("cpOriginalPrice");
    }

    public void AddConsumerFacingHierarchy()
    {
        parameters.Add("consumerFacingHierarchy");
    }

    public void AddCategories()
    {
        parameters.Add("categories");
    }

    public void AddSearchHierarchy()
    {
        parameters.Add("searchHierchy");
        // Typo in the Salling API
    }

    public void AddPant()
    {
        parameters.Add("pant");
    }

    public void AddInStockStore()
    {
        parameters.Add("inStockStore");
    }

    public void AddOutStockStore()
    {
        parameters.Add("outStockStore");
    }

    public void AddAttributes()
    {
        parameters.Add("attributes");
    }

    public void AddDeepestCategoryPath()
    {
        parameters.Add("deepestCategoryPath");
    }
}