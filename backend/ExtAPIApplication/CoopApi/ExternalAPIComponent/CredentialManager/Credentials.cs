using System.Text.Json;
using Newtonsoft.Json;
using Serilog;

namespace ExternalAPIComponent;

/*
 * Singleton patterned class to acquire and store secrets.
 *
 * Default path: {PROJECT_DIR}/api-key.txt
 */
public class Credentials
{
    public Dictionary<string, string> Keys { get; } = new();

    private readonly string _apiKeyPath = Path.Combine(Directory.GetCurrentDirectory(), "api-keys.json");

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

    public static Credentials Instance { get; } = new();
}