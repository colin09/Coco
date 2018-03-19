using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

using com.mh.common.fileLogger;

namespace com.mh.common.Logger
{
    public class Logging : ILog
    {

        private ILogger logger;

        public Logging()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/configurations/")
                .AddJsonFile(@"logging.json", optional: true, reloadOnChange: true);
            //.AddJsonFile(@"Configurations\logging.json");
            //.AddEnvironmentVariables();



            ILoggerFactory factory = new LoggerFactory()
                .AddFile(builder.Build().GetSection("FileLogging"));
            // .AddConsole()
            // .AddDebug();

            logger = factory.CreateLogger<Logging>();
        }



        public void Debug(object obj)
        {
            logger.Log(LogLevel.Debug,
                new EventId(0, "#"),
                obj,
                null,
                (msg, ex) =>
                {
                    return msg.ToString();
                });
        }

        public void Error(object obj)
        {
            logger.Log(LogLevel.Error,
                new EventId(0, "#"),
                obj,
                null,
                (msg, ex) =>
                {
                    return msg.ToString();
                });
        }

        public void Exception(object obj)
        {
            logger.Log(LogLevel.Error,
                new EventId(0, "#"),
                obj,
                null,
                (msg, ex) =>
                {
                    return msg.ToString();
                });
        }

        public void Info(object obj)
        {
            logger.Log(LogLevel.Information,
                new EventId(0, "#"),
                obj,
                null,
                (msg, ex) =>
                {
                    return msg.ToString();
                });
        }

        public void Warn(object obj)
        {
            logger.Log(LogLevel.Warning,
                new EventId(0, "#"),
                obj,
                null,
                (msg, ex) =>
                {
                    return msg.ToString();
                });
        }
    }
}