using System.Diagnostics;
using ExternalApiLibrary.DTO;

namespace ExternalApiLibrary.Models;

[DebuggerDisplay("{Name}, {RetailGroupName}, {Address}")]
public class FilteredCoopStore : IFilteredDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Kardex { get; set; }
    public int Zipcode { get; set; }
    public string City { get; set; }
    public List<double> Location { get; set; }
    public string RetailGroupName { get; set; }
}