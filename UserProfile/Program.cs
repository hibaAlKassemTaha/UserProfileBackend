using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.Helpers;

namespace UserProfile
{
    public class Program
    {
        static ILogger Logger { get; } =
        ApplicationLogging.CreateLogger<Program>();
        public static void Main(string[] args)
        {
            ApplicationLogging.LoggerFactory.AddProvider(
  new ConsoleLoggerProvider(
      new DummyConsoleLoggerOptionsMonitor(LogLevel.Information)));
            Logger.LogInformation(
              "A user profile web api project has been initialized");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://localhost:4000");
                });
    }
}
