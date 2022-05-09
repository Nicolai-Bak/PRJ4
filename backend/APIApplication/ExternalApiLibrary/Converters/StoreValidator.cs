using DatabaseLibrary.Models;
using ExternalApiLibrary.Models;
using Serilog;

namespace ExternalApiLibrary.Converters;

public enum StoreGroup
{
	Salling,
	Coop
}

public static class StoreValidator
{
	public static List<IDbModelsDto> ValidateStores(List<Store>? stores, StoreGroup storeGroup)
    {
        var validStores = new List<IDbModelsDto>();

        stores?.ForEach(store =>
        {
    	    bool isValid = true;
    	    
    	    if (store.ID <= 0)
    	    {
    		    LogInvalidStore(store, $"ID was less than or equal to 0", storeGroup);
    		    isValid = false;
    	    }
    	    if (string.IsNullOrEmpty(store.Brand) || string.IsNullOrWhiteSpace(store.Brand))
    	    {
    		    LogInvalidStore(store, $"Brand was empty", storeGroup);
    		    isValid = false;
    	    }
    	    if (store.Location_X <= 0)
    	    {
    		    LogInvalidStore(store, $"X-coordinate was less than or equal to 0", storeGroup);
    		    isValid = false;
    	    }
    	    if (store.Location_Y <= 0)
    	    {
    		    LogInvalidStore(store, $"Y-coordinate was less than or equal to 0", storeGroup);
    		    isValid = false;
    	    }
    	    if (string.IsNullOrEmpty(store.Address) || string.IsNullOrWhiteSpace(store.Address))
    	    {
    		    LogInvalidStore(store, $"Address was empty", storeGroup);
    		    isValid = false;
    	    }

            if (isValid)
    		    validStores.Add(store);
        });

        return validStores;
    }
    
    private static void LogInvalidStore(Store store, string msg, StoreGroup storeGroup)
    {
        Log.Fatal($"[{Enum.GetName(typeof(StoreGroup), storeGroup) + "StoreConverter"}]] " +
                  $"Invalid store conversion error: {msg}\nStore: {store}");
    }
}
