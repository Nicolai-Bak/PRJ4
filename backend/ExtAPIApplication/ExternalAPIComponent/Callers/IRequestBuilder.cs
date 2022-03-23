namespace ExternalAPIComponent.Callers;

public interface IRequestBuilder
{
    public IRequest Build();


    #region Product Images

    /**
     * Images in the form of a full URL
     * Type: Array of strings
     * Example: https://dsdam.imgix.net/services/assets.img/id/c1899e4f-ca81-4511-a0fe-2dbeb213ffa5/size/DEFAULT.jpg
     */
    public void AddImages();

    /**
     * GUIDs for image URL of the product
     * Type: Array of strings
     * Example: c1899e4f-ca81-4511-a0fe-2dbeb213ffa5
     */
    public void AddImageGuids();

    #endregion

    #region Product Information

    /**
     * Short description of the product
     * Type: String
     * Example: Bananer
     */
    public void AddProductType();

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
    public void AddStoreData();

    /**
     * Short description of the product
     * Type: string
     * Example: Bananer 4 pak øko
     */
    public void AddDescription();

    /**
     * A list of infos - Expanded Description, Nutritional and Product details
     * Type: Array
     * Example:
     * Expanded Description: A long description of the product
     * Nutritional (100g): Energy (kCal/kJ), Fat, Carbs, Protein
     * Product Details: Weight, Product type, EAN, Article, PID
     */
    public void AddInfos();

    /**
     * Numerical value of weight, piece, volume etc. as determined by UnitsOfMeasure
     * Type: Int
     * Example: 4
     */
    public void AddUnits();

    /**
     * Unit of measurement for the product
     * Type: String
     * Example: stk
     */
    public void AddUnitsOfMeasure();

    /**
     * Country of Origin - Can be null
     * Type: String
     * Example: Spanien
     */
    public void AddCountryOfOrigin();

    /**
     * List of product attributes like Fairtrade, Økologisk etc.
     * Type: String
     * Example: Fairtrade
     */
    public void AddProperties();

    #endregion

    #region Product Identifiers

    /**
     * Name of the product
     * Type: String
     * Example: Bananer 4 pak øko
     */
    public void AddName();

    /**
     * Alternative name of the product - Not always the same as Name
     * Type: String
     * Exmaple: Økologiske bananer
     */
    public void AddProductName();

    /**
     * Internal identifier for the product
     * Type: Int
     * Example: 89300505023
     */
    public void AddArticle();

    /**
     * Brand of the product - May be null
     * Type: String
     * Example: Salling ØKO
     */
    public void AddBrand();

    /**
     * Alternative brand of the product - May be null
     * Type: String
     * Example: Vores
     */
    public void AddSubBrand();

    /**
     * Internal identifier for the object of the product - Same as used in product URL
     * Type: String
     * Example: 72008
     */
    public void AddObjectId();

    /**
     * Store IDs of the stores who has the product on discount
     * Type: Array of Ints
     */
    public void AddIsInOffer();

    #endregion

    #region Member Discount Information

    /**
     * Whether the product has an active member discount
     * Type: boolean
     */
    public void AddCpOffer();

    /**
     * Price of the product including discount
     * Type: Int
     */
    public void AddCpOfferPrice();

    /**
     * Amount of products required to purchase in bulk in order to satisfy the discount conditions
     * Type: Int
     * Example: 1
     */
    public void AddCpOfferAmount();

    /**
     * Identifier for the offer - Matching ids mean product mixing allowed
     * Type: Int
     */
    public void AddCpOfferId();

    /**
     * Start of offer period
     * Type: String
     * Example: 2022-03-18
     */
    public void AddCpOfferFromDate();

    /**
     * End of offer period
     * Type: String
     * Example: 2022-03-25
     */
    public void AddCpOfferToDate();

    /**
     * Title of the offer
     * Type: String
     * Example: Salling økologisk hvid spidskål
     */
    public void AddCpOfferTitle();

    /**
     * The price reduced by a fixed numerical amount
     * Type: Int
     * Example: 600
     */
    public void AddCpDiscount();

    /**
     * The price reduced by a fixed percentage
     * Type: Int
     * Example: 33
     */
    public void AddCpPercentDiscount();

    /**
     * Original price of the product
     * Type: Int
     * Example 1500
     */
    public void AddCpOriginalPrice();

    #endregion

    #region Misc. Product Information

    /**
     * Breadcrums to the category of the product
     * Type: Array
     */
    public void AddConsumerFacingHierarchy();

    /**
     * See AddConsumerFacingHierarchy()
     */
    public void AddCategories();

    /**
     * Related search terms of the product
     * Type: Array of Objects
     */
    public void AddSearchHierarchy(); // Hierachy

    /**
     * Whether the product has a deposit
     * Type: boolean
     */
    public void AddPant();

    /**
     * List of stores that has the product in stock
     * Type: Array
     */
    public void AddInStockStore();

    /**
     * List of stores that do not have the product in stock
     * Type: Array
     */
    public void AddOutStockStore();

    /**
     * Miscellaneous product names
     * Type: Array
     */
    public void AddAttributes();

    /**
     * Deepest breadcrumb
     * Type: String
     */
    public void AddDeepestCategoryPath();

    #endregion
}