

using System;
using Microsoft.Extensions.Logging;

namespace com.fs.api.Models{

    public class MyLogProvider : ILoggerProvider
    {
        public MyLogProvider()
        {
        }

        ILogger ILoggerProvider.CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }



}