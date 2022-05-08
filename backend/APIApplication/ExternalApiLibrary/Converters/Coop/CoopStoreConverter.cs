using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;
using Serilog;

namespace ExternalApiLibrary.Converters.Coop;

public class CoopStoreConverter : IConverter
{
    public List<IDbModelsDto> Convert(List<IFilteredDto> list)
    {
        var filteredList = ValidateStores(list.Cast<FilteredCoopStore>().ToList());
        
        var stores = filteredList.Select(store => new Store
        {
            ID = store.Kardex,
            Brand = store.RetailGroupName,
            Location_X = store.Location[0],
            Location_Y = store.Location[1],
            Address = $"{store.Address}, {store.Zipcode} {store.City}"
        }).ToList();

        return new List<IDbModelsDto>(stores);
    }

    private List<FilteredCoopStore> ValidateStores(List<FilteredCoopStore>? stores)
    {
	    var validStores = new List<FilteredCoopStore>();

	    stores?.ForEach(store =>
	    {
		    bool isValid = true;
		    
		    if (store.Kardex <= 0)
		    {
			    LogInvalidStore(store, $"Kardex was less than or equal to 0");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(store.RetailGroupName) || string.IsNullOrWhiteSpace(store.RetailGroupName))
		    {
			    LogInvalidStore(store, $"Retail Group Name was empty");
			    isValid = false;
		    }
		    if (store.Location[0] <= 0)
		    {
			    LogInvalidStore(store, $"X-coordinate was less than or equal to 0");
			    isValid = false;
		    }
		    if (store.Location[1] <= 0)
		    {
			    LogInvalidStore(store, $"Y-coordinate was less than or equal to 0");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(store.Address) || string.IsNullOrWhiteSpace(store.Address))
		    {
			    LogInvalidStore(store, $"Address was empty");
			    isValid = false;
		    }
		    if (store.Zipcode <= 0)
		    {
			    LogInvalidStore(store, $"Zipcode was less than or equal to 0");
			    isValid = false;
		    }
		    if (string.IsNullOrEmpty(store.City) || string.IsNullOrWhiteSpace(store.City))
		    {
			    LogInvalidStore(store, $"City was empty");
			    isValid = false;
		    }


		    if (isValid)
			    validStores.Add(store);
	    });

	    return validStores;
    }
    
    private void LogInvalidStore(FilteredCoopStore store, string msg)
    {
	    Log.Fatal("[CoopStoreConverter] Invalid store conversion error: {@msg}\nStore: {@store}",
		    msg, store);
    }
 
}