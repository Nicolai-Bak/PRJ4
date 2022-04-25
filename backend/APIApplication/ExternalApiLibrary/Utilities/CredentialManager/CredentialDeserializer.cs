using Newtonsoft.Json;
using Serilog;

namespace ExternalApiLibrary.Utilities.CredentialManager;

public static class CredentialDeserializer
{
    public static Dictionary<string, string> AcquireCredentials(string path)
    {
        Dictionary<string, string> Keys = new();

        if (!File.Exists(path))
        {
            Log.Fatal("Error acquiring credentials - No API key file found. " +
                      "Place {fileName} at {path}.", Path.GetFileName(path), path);
            return Keys;
        }

        try
        {
            Log.Information("Reading credentials from {Path}", path);

            var json = File.ReadAllText(path);
            if (json.Length == 0)
            {
                Log.Fatal("Error reading credentials");
                return Keys;
            }

            var deserializedJson = JsonConvert.DeserializeObject<ApiKeys>(json);
            if (deserializedJson!.Keys.Count == 0)
            {
                Log.Warning("No credentials were found");
                return Keys;
            }

            foreach (var api in deserializedJson.Keys)
            {
                Log.Information("Read key from {Name}: {Key}",
                    api.Name,
                    api.Key);

                Keys.Add(api.Name, api.Key);
            }

            return Keys;
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Error acquiring credentials");
            throw;
        }
    }

    public class ApiKeys
    {
        [JsonProperty("ApiKeys")]
        public List<ApiKey> Keys { get; set; } = new();
    }

    public class ApiKey
    {
        public string Name { get; set; } = "";
        public string Key { get; set; } = "";
    }
}

