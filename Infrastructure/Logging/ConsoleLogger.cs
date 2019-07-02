using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = AppCore.Interfaces.ILogger;

namespace Infrastructure.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void LogException(Exception ex)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            Log.Error(ex, ex.Message);
        }
    }
}
