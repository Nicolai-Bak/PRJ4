using Algolia.Search.Clients;

namespace ExternalAPIComponent;

public abstract class ICaller
{
    public string Call(IRequestBuilder requestBuilder)
    {
    }
}

public class RequestBuilder : IRequestBuilder
{
    private readonly SearchClient client;
    private readonly SearchIndex index;

    private List<string> paramters;

    public RequestBuilder()
    {
        var credentials = Credentials.Instance;

        client = new SearchClient(credentials.Keys["Salling-Application"], 
            credentials.Keys["Salling-Admin"]);

        index = client.InitIndex(credentials.Keys["Salling-IndexName"]);
    }

    public string Build()
    {
        throw new NotImplementedException();
    }

    public void AddImages()
    {
        throw new NotImplementedException();
    }

    public void AddImageGuids()
    {
        throw new NotImplementedException();
    }

    public void AddProductType()
    {
        throw new NotImplementedException();
    }

    public void AddStoreData()
    {
        throw new NotImplementedException();
    }

    public void AddDescription()
    {
        throw new NotImplementedException();
    }

    public void AddInfos()
    {
        throw new NotImplementedException();
    }

    public void AddUnits()
    {
        throw new NotImplementedException();
    }

    public void AddUnitsOfMeasure()
    {
        throw new NotImplementedException();
    }

    public void AddCountryOfOrigin()
    {
        throw new NotImplementedException();
    }

    public void AddProperties()
    {
        throw new NotImplementedException();
    }

    public void AddName()
    {
        throw new NotImplementedException();
    }

    public void AddProductName()
    {
        throw new NotImplementedException();
    }

    public void AddArticle()
    {
        throw new NotImplementedException();
    }

    public void AddBrand()
    {
        throw new NotImplementedException();
    }

    public void AddSubBrand()
    {
        throw new NotImplementedException();
    }

    public void AddObjectId()
    {
        throw new NotImplementedException();
    }

    public void AddIsInOffer()
    {
        throw new NotImplementedException();
    }

    public void AddCpOffer()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferPrice()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferAmount()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferId()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferFromDate()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferToDate()
    {
        throw new NotImplementedException();
    }

    public void AddCpOfferTitle()
    {
        throw new NotImplementedException();
    }

    public void AddCpDiscount()
    {
        throw new NotImplementedException();
    }

    public void AddCpPercentDiscount()
    {
        throw new NotImplementedException();
    }

    public void AddCpOriginalPrice()
    {
        throw new NotImplementedException();
    }

    public void AddConsumerFacingHierarchy()
    {
        throw new NotImplementedException();
    }

    public void AddCategories()
    {
        throw new NotImplementedException();
    }

    public void AddSearchHierarchy()
    {
        throw new NotImplementedException();
    }

    public void AddPant()
    {
        throw new NotImplementedException();
    }

    public void AddInStockStore()
    {
        throw new NotImplementedException();
    }

    public void AddOutStockStore()
    {
        throw new NotImplementedException();
    }

    public void AddAttributes()
    {
        throw new NotImplementedException();
    }

    public void AddDeepestCategoryPath()
    {
        throw new NotImplementedException();
    }
}