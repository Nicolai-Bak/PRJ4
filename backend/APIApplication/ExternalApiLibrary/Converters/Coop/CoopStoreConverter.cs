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
        var filteredList = list.Cast<FilteredCoopStore>().ToList();
        
        var stores = filteredList.Select(store => new Store
        {
            ID = store.Kardex,
            Brand = store.RetailGroupName,
            Location_X = store.Location[0],
            Location_Y = store.Location[1],
            Address = $"{store.Address}, {store.Zipcode} {store.City}"
        }).ToList();

        return StoreValidator.ValidateStores(stores, StoreGroup.Coop);
    }

}