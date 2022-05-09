using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.DTO;
using ExternalApiLibrary.Models;

namespace ExternalApiLibrary.Converters.Salling;

public class SallingStoreConverter : IConverter
{
    public List<IDbModelsDto> Convert(List<IFilteredDto> list)
    {
        var filteredList = list.Cast<FilteredSallingStore>().ToList();

        var stores = filteredList.Select(store => new Store
        {
            ID = store.Id,
            Brand = store.Brand,
            Location_X = store.Coordinates[0],
            Location_Y = store.Coordinates[1],
            Address = store.AddressField.Street + ", " +
                      store.AddressField.Zip + " " +
                      store.AddressField.City + ", " +
                      store.AddressField.Country
        }).ToList();
        
        stores = stores.Where(s=>s.Brand== "foetex").ToList();

        return StoreValidator.ValidateStores(stores, StoreGroup.Salling);
    }
}
