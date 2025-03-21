using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.File;

namespace EcommerceLiveEfCore.Services
{
    public class LoggerService
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console() 
                .WriteTo.File("Logs/log_.txt", rollingInterval: RollingInterval.Day) // This requires Serilog.Sinks.File
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }
    }
}
