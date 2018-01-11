


using Microsoft.Extensions.Logging;

namespace com.fs.api.Models{


    public class MyLogger : ILoggerFactory
    {
        public MyLogger()
        {
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new System.NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }


}