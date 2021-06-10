using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace UserProfile.Helpers
{
    class DummyConsoleLoggerOptionsMonitor : IOptionsMonitor<ConsoleLoggerOptions>
    {
        private readonly ConsoleLoggerOptions option = new ConsoleLoggerOptions();

        public DummyConsoleLoggerOptionsMonitor(LogLevel level)
        {
            option.LogToStandardErrorThreshold = level;
        }

        public ConsoleLoggerOptions Get(string name)
        {
            return this.option;
        }

        public IDisposable OnChange(Action<ConsoleLoggerOptions, string> listener)
        {
            return new DummyDisposable();
        }

        public ConsoleLoggerOptions CurrentValue => this.option;

        private sealed class DummyDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
