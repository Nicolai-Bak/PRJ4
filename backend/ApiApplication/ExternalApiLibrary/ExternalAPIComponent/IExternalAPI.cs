using ExternalAPIComponent.Callers.Interfaces;
using ExternalApiLibrary.ExternalAPIComponent.Callers.Salling;
using ExternalApiLibrary.ExternalAPIComponent.Converters;
using ExternalApiLibrary.ExternalAPIComponent.Filters;

namespace ExternalAPIComponent;

public abstract class ExternalAPI
{
    public ICaller caller { get; set; }
    public IFilter filter { get; set; }
    public IConverter converter { get; set; }

    public List<object> Get()
    {
        return Convert(Filter(Call()));
    }
    protected abstract List<object> Call();
    protected abstract List<object> Filter(List<object> unfilteredResponse);
    protected abstract List<object> Convert(List<object> filteredResponse);
}

public interface IApiFactory
{
    public ICaller CreateCaller();
    public IFilter CreateFilter();
    public IConverter CreateConverter();
}

public class FøtexFactory : IApiFactory
{
    public ICaller CreateCaller()
    {
        SallingProductCaller productCaller = new();
        SallingRequestBuilder builder = new SallingRequestBuilder();
        builder.AddInfos()
            .AddUnits()
            .AddUnitsOfMeasure()
            .AddStoreData();

        return productCaller;
    }

    public IFilter CreateFilter()
    {
        return  new SallingProductFilter();
    }

    public IConverter CreateConverter()
    {
        throw new NotImplementedException();
    }
}

//public class ConcreteExternalApi
//{

//    public ICaller caller { get; set; }
//    public IFilter filter { get; set; }
//    public IConverter converter { get; set; }

//    public ConcreteExternalApi()
//    {
        
//    }

//    public List<object> Get()
//    {
//        return Convert(Filter(Call()));
//    }

//    private List<object> Call()
//    {
        
//    }
//    private List<object> Filter(List<object> unfilteredResponse)
//    {
        
//    }

//    private List<object> Convert(List<object> filteredResponse)
//    {

//    }
//}