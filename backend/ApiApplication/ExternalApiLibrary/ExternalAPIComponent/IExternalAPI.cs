namespace ExternalAPIComponent;

public abstract class IExternalAPI
{
    public string Get()
    {
        return Convert(Filter(Call()));
    }

    protected abstract string Call();
    protected abstract string Filter(string unfilteredResponse);
    protected abstract string Convert(string filteredResponse);
}