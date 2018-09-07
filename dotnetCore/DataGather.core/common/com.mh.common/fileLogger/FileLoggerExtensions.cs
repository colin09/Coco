using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace com.mh.common.fileLogger
{
    public static class FileLoggerExtensions
    {
        //add 日志文件创建规则，分割规则，格式化规则，过滤规则 to appsettings.json
        public static ILoggerFactory AddFile(this ILoggerFactory factory, IConfiguration configuration)
        {
            return AddFile(factory, new FileLoggerSettings(configuration));
        }

        
        public static ILoggerFactory AddFile(this ILoggerFactory factory, FileLoggerSettings fileLoggerSettings)
        {
            factory.AddProvider(new FileLoggerProvider(fileLoggerSettings));
            return factory;
        }
    }

}