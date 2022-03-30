using ExternalAPIComponent;
using Serilog;

namespace ExternalApiLibrary.ExternalAPIComponent.Utilities.CredentialManager;

/*
 * Singleton patterned class to acquire and store secrets.
 *
 * Default path: {PROJECT_DIR}/Utilities/api-keys.json
 */
public class Credentials
{
    private readonly string _apiKeyPath = Path.Combine(
        Path.GetFullPath(@"..\..\..\"),
        "ExternalAPIComponent",
        "Utilities",
        "CredentialManager",
        "api-keys.json");

    private Credentials()
    {
        try
        {
            Keys = CredentialDeserializer.AcquireCredentials(_apiKeyPath);
            Log.Information("Acquired credentials: {@Key}", Keys);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Error in acquiring credentials");
            throw;
        }
    }

    /**
     * Every Key is stored within this dictionary.
     */
    public Dictionary<string, string> Keys { get; } = new();

    public static Credentials Instance { get; } = new();
}