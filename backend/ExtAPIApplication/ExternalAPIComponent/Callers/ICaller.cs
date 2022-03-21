namespace ExternalAPIComponent.Callers;

public abstract class Caller
{
    public abstract string Call(IRequestBuilder requestBuilder);
}