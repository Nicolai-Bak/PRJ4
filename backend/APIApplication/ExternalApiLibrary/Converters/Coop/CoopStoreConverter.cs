using ExternalApiLibrary.ExternalAPIComponent.Converters.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Filters.Coop;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters.Coop;

public class CoopStoreConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
	    var filteredList = list.Cast<List<FilteredCoopStore>>().ToList();
	    
	    var flattenedList = (from flatList in filteredList
		    from item in flatList
		    select item).ToList();

	    var stores = flattenedList.Select(store => new ConvertedCoopStore
	    {
		    ID = store.Kardex,
		    Brand = store.RetailGroupName,
		    Location_X = store.Location[0],
		    Location_Y = store.Location[1],
		    Address = $"{store.Address}, {store.Zipcode} {store.City}"
	    });

	    return new List<object>(stores);
    }
}

public class ConvertedCoopStore
{
	public int ID { get; set; }
	public string Brand { get; set; }
	public double Location_X { get; set; }
	public double Location_Y { get; set; }
	public string Address { get; set; }
}

