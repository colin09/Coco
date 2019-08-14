using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace T.Host {
    public class MyLog : ILoggingBuilder {
        public IServiceCollection Services =>
            throw new System.NotImplementedException ();
    }

    public static class LoggerExtensions {
        public static ILoggingBuilder AddMylog (this ILoggingBuilder builder) {
builder.
        }

    }

}