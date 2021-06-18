using System;
using Microsoft.Extensions.Logging;
using Console = System.Console;

namespace TouchPortalSDK.Extensions.Attributes.Tests.FakeLogger
{
    public class Logger : ILogger
    {
        private readonly string _categoryName;

        public Logger(string categoryName)
        {
            _categoryName = categoryName;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var line = formatter(state, exception);
            Console.WriteLine(line);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
