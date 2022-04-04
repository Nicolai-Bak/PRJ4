using ExternalApiLibrary.ExternalAPIComponent.Filters;

namespace ExternalApiLibrary.ExternalAPIComponent.Converters;

public class SallingStoreConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        List<FilteredSallingStore> filteredList = list.Cast<FilteredSallingStore>().ToList();

        var stores = filteredList.Select(store => new ConvertedSallingStore()
        {
            ID = store.Id,
            Brand = store.Brand,
            Location_X = store.Coordinates[0],
            Location_Y = store.Coordinates[1],
            Address = store.Address.Street + ", " + store.Address.Zip + " " + store.Address.City + ", " + store.Address.Country
        }).ToList();

        return new List<object>(stores);
    }
}

public class ConvertedSallingStore
{
    public int ID { get; set; }
    public string Brand { get; set; }
    public double Location_X { get; set; }
    public double Location_Y { get; set; }
    public string Address { get; set; }
}
