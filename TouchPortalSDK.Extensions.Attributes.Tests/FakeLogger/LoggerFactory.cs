using Microsoft.Extensions.Logging;

namespace TouchPortalSDK.Extensions.Attributes.Tests.FakeLogger
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            //
        }

        public void Dispose()
        {
            //
        }
    }
}
