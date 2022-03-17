using Serilog;

namespace ExternalAPIComponent;

static class BackendLogger
{
    public static void BuildLogger()
    {
        // Set up configuration file for logger
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        // Build logger from configuration
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }
}