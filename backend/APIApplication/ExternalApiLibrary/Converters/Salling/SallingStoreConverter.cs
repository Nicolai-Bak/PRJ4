using DatabaseLibrary.Models;
using ExternalApiLibrary.Converters.Interfaces;
using ExternalApiLibrary.Filters.Models;

namespace ExternalApiLibrary.Converters.Salling;

public class SallingStoreConverter : IConverter
{
    public List<object> Convert(List<object> list)
    {
        List<FilteredSallingStore> filteredList = list.Cast<FilteredSallingStore>().ToList();

        var stores = filteredList.Select(store => new Store
        {
            ID = store.Id,
            Brand = store.Brand,
            Location_X = store.Coordinates[0],
            Location_Y = store.Coordinates[1],
            Address = store.AddressField.Street + ", " + store.AddressField.Zip + " " + store.AddressField.City + ", " + store.AddressField.Country
        }).ToList();

        return new List<object>(stores);
    }
}

//public class ConvertedSallingStore
//{
//    public int ID { get; set; }
//    public string Brand { get; set; }
//    public double Location_X { get; set; }
//    public double Location_Y { get; set; }
//    public string Address { get; set; }
//}
