using Algolia.Search.Clients;
using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Utilities.CredentialManager;

namespace ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;

/**
 * Builds a request from a builder pattern of parameters
 */
public class SallingRequestBuilder : IRequestBuilder
{
    private readonly SallingRequest _newRequest;
    private readonly List<string> _parameters = new();

    public SallingRequestBuilder()
    {
        var credentials = Credentials.Instance;

        var client = new SearchClient(
            credentials.Keys["Salling-Application"],
            credentials.Keys["Salling-Admin"]);

        var index = client.InitIndex(credentials.Keys["Salling-IndexName"]);

        _newRequest = new SallingRequest(index);
    }

    public IRequest Build()
    {
        _newRequest.Parameters = _parameters;
        return _newRequest;
    }
    
    #region Product Images
    /**
     * Images in the form of a full URL
     * Type: Array of strings
     * Example: https://dsdam.imgix.net/services/assets.img/id/c1899e4f-ca81-4511-a0fe-2dbeb213ffa5/size/DEFAULT.jpg
     */
    public SallingRequestBuilder AddImages()
    {
        _parameters.Add("images");
        return this;
    }

    /**
     * GUIDs for image URL of the product
     * Type: Array of strings
     * Example: c1899e4f-ca81-4511-a0fe-2dbeb213ffa5
     */
    public SallingRequestBuilder AddImageGuids()
    {
        _parameters.Add("imageGUIDs");
        return this;
    }
    #endregion
    
    #region Product Information
    /**
     * Short description of the product
     * Type: String
     * Example: Bananer
     */
    public SallingRequestBuilder AddProductType()
    {
        _parameters.Add("productType");
        return this;
    }

    /**
     * *  List of inStock, price, unitPrice and unitOfMeasurement for every store
     * *  Type: Array
     * *  Example:
     * * "1373": {
     * "inStock": true,
     * "multipromo": "",
     * "offerDescription": "Tilbud",
     * "price": 1500,
     * "multiPromoPrice": "",
     * "unitsOfMeasurePrice": 2416,
     * "unitsOfMeasurePriceUnit": "Kg.",
     * "unitsOfMeasureOfferPrice": 1579,
     * "unitsOfMeasureShowPrice": 1579
     * },
     */
    public SallingRequestBuilder AddStoreData()
    {
        _parameters.Add("storeData");
        return this;
    }

    /**
     * Short description of the product
     * Type: string
     * Example: Bananer 4 pak øko
     */
    public SallingRequestBuilder AddDescription()
    {
        _parameters.Add("description");
        return this;
    }
    
    /**
     * A list of infos - Expanded Description, Nutritional and Product details
     * Type: Array
     * Example:
     * Expanded Description: A long description of the product
     * Nutritional (100g): Energy (kCal/kJ), Fat, Carbs, Protein
     * Product Details: Weight, Product type, EAN, Article, PID
     */
    public SallingRequestBuilder AddInfos()
    {
        _parameters.Add("infos");
        return this;
    }

    /**
     * Numerical value of weight, piece, volume etc. as determined by UnitsOfMeasure
     * Type: Int
     * Example: 4
     */
    public SallingRequestBuilder AddUnits()
    {
        _parameters.Add("units");
        return this;
    }

    /**
     * Unit of measurement for the product
     * Type: String
     * Example: stk
     */
    public SallingRequestBuilder AddUnitsOfMeasure()
    {
        _parameters.Add("unitsOfMeasure");
        return this;
    }

    /**
     * Country of Origin - Can be null
     * Type: String
     * Example: Spanien
     */
    public SallingRequestBuilder AddCountryOfOrigin()
    {
        _parameters.Add("countryOfOrigin");
        return this;
    }

    /**
     * List of product attributes like Fairtrade, Økologisk etc.
     * Type: String
     * Example: Fairtrade
     */
    public SallingRequestBuilder AddProperties()
    {
        _parameters.Add("properties");
        return this;
    }
    #endregion
    
    #region Product Identifiers
    /**
     * Name of the product
     * Type: String
     * Example: Bananer 4 pak øko
     */
    public SallingRequestBuilder AddName()
    {
        _parameters.Add("name");
        return this;
    }

    /**
     * Alternative name of the product - Not always the same as Name
     * Type: String
     * Exmaple: Økologiske bananer
     */
    public SallingRequestBuilder AddProductName()
    {
        _parameters.Add("productName");
        return this;
    }

    /**
     * Internal identifier for the product
     * Type: Int
     * Example: 89300505023
     */
    public SallingRequestBuilder AddArticle()
    {
        _parameters.Add("article");
        return this;
    }

    /**
     * Brand of the product - May be null
     * Type: String
     * Example: Salling ØKO
     */
    public SallingRequestBuilder AddBrand()
    {
        _parameters.Add("brand");
        return this;
    }

    /**
     * Alternative brand of the product - May be null
     * Type: String
     * Example: Vores
     */
    public SallingRequestBuilder AddSubBrand()
    {
        _parameters.Add("subBrand");
        return this;
    }

    /**
     * Internal identifier for the object of the product - Same as used in product URL
     * Type: String
     * Example: 72008
     */
    public SallingRequestBuilder AddObjectId()
    {
        _parameters.Add("objectId");
        return this;
    }

    /**
     * Store IDs of the stores who has the product on discount
     * Type: Array of Ints
     */
    public SallingRequestBuilder AddIsInOffer()
    {
        _parameters.Add("isInOffer");
        return this;
    }
    #endregion

    #region Member Discount Information
    /**
     * Whether the product has an active member discount
     * Type: boolean
     */
    public SallingRequestBuilder AddCpOffer()
    {
        _parameters.Add("cpOffer");
        return this;
    }

    /**
     * Price of the product including discount
     * Type: Int
     */
    public SallingRequestBuilder AddCpOfferPrice()
    {
        _parameters.Add("cpOfferPrice");
        return this;
    }

    /**
     * Amount of products required to purchase in bulk in order to satisfy the discount conditions
     * Type: Int
     * Example: 1
     */
    public SallingRequestBuilder AddCpOfferAmount()
    {
        _parameters.Add("cpOfferAmount");
        return this;
    }

    /**
     * Identifier for the offer - Matching ids mean product mixing allowed
     * Type: Int
     */
    public SallingRequestBuilder AddCpOfferId()
    {
        _parameters.Add("cpOfferId");
        return this;
    }

    /**
     * Start of offer period
     * Type: String
     * Example: 2022-03-18
     */
    public SallingRequestBuilder AddCpOfferFromDate()
    {
        _parameters.Add("cpOfferFromDate");
        return this;
    }

    /**
     * End of offer period
     * Type: String
     * Example: 2022-03-25
     */
    public SallingRequestBuilder AddCpOfferToDate()
    {
        _parameters.Add("cpOfferToDate");
        return this;
    }

    /**
     * Title of the offer
     * Type: String
     * Example: Salling økologisk hvid spidskål
     */
    public SallingRequestBuilder AddCpOfferTitle()
    {
        _parameters.Add("cpOfferTitle");
        return this;
    }

    /**
     * The price reduced by a fixed numerical amount
     * Type: Int
     * Example: 600
     */
    public SallingRequestBuilder AddCpDiscount()
    {
        _parameters.Add("cpDiscount");
        return this;
    }

    /**
     * The price reduced by a fixed percentage
     * Type: Int
     * Example: 33
     */
    public SallingRequestBuilder AddCpPercentDiscount()
    {
        _parameters.Add("cpPercentDiscount");
        return this;
    }

    /**
     * Original price of the product
     * Type: Int
     * Example 1500
     */
    public SallingRequestBuilder AddCpOriginalPrice()
    {
        _parameters.Add("cpOriginalPrice");
        return this;
    }
    #endregion
    
    #region Misc. Product Information

    /**
     * Breadcrumbs to the category of the product
     * Type: Array
     */
    public SallingRequestBuilder AddConsumerFacingHierarchy()
    {
        _parameters.Add("consumerFacingHierarchy");
        return this;
    }

    /**
     * See AddConsumerFacingHierarchy()
     */
    public SallingRequestBuilder AddCategories()
    {
        _parameters.Add("categories");
        return this;
    }

    /**
     * Related search terms of the product
     * Type: Array of Objects
     */
    public SallingRequestBuilder AddSearchHierarchy()
    {
        // Typo in the Salling API
        _parameters.Add("searchHierchy");
        return this;
    }

    /**
     * Whether the product has a deposit
     * Type: boolean
     */
    public SallingRequestBuilder AddPant()
    {
        _parameters.Add("pant");
        return this;
    }

    /**
     * List of stores that has the product in stock
     * Type: Array
     */
    public SallingRequestBuilder AddInStockStore()
    {
        _parameters.Add("inStockStore");
        return this;
    }

    /**
     * List of stores that do not have the product in stock
     * Type: Array
     */
    public SallingRequestBuilder AddOutStockStore()
    {
        _parameters.Add("outStockStore");
        return this;
    }

    /**
     * Miscellaneous product names
     * Type: Array
     */
    public SallingRequestBuilder AddAttributes()
    {
        _parameters.Add("attributes");
        return this;
    }

    /**
     * Deepest breadcrumb
     * Type: String
     */
    public SallingRequestBuilder AddDeepestCategoryPath()
    {
        _parameters.Add("deepestCategoryPath");
        return this;
    }
    #endregion
}