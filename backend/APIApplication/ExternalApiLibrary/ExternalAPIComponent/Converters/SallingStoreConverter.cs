using ApiApplication.Database.Models;
using ExternalApiLibrary.ExternalAPIComponent.Filters;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters;

public class SallingStoreConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        List<FilteredSallingStore> filteredList = list.Cast<FilteredSallingStore>().ToList();

        var stores = filteredList.Select(store => new Store()
        {
            ID = store.Id,
            Brand = store.Brand,
            Location_X = store.Coordinates[0].Value,
            Location_Y = store.Coordinates[1].Value,
            Address = store.Address.Street + ", " + store.Address.Zip + " " + store.Address.City + ", " + store.Address.Country
        }).ToList();

        return new List<object>(stores);
    }
}