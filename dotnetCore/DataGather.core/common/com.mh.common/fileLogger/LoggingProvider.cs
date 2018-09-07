using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace com.mh.common.fileLogger
{

    public class LoggingProvider : ILoggerProvider, IDisposable
    {
        FileLoggerSettings _configuration;
        readonly ConcurrentDictionary<string, InitLoggerModel> _loggerKeys = new ConcurrentDictionary<string, InitLoggerModel>();
        readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>(); public LoggingProvider()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            var loggerKey = this._loggerKeys.GetOrAdd(categoryName, p =>
            {
                InitLoggerModel model = new InitLoggerModel();
                InitLoggerSettings(categoryName, model);
                return model;
            });
            var key = loggerKey.FileDiretoryPath + loggerKey.FileNameTemplate;
            return this._loggers.GetOrAdd(key, p =>
            {
                var logger = new FileLogger(categoryName);
                InitLogger(loggerKey, logger);
                return logger;
            });
        }

        private static void InitLogger(InitLoggerModel model, FileLogger logger)
        {
            logger.FileNameTemplate = model.FileNameTemplate;
            logger.FileDiretoryPath = model.FileDiretoryPath;
            logger.MinLevel = model.MinLevel;
        }


        private void InitLoggerSettings(string categoryName, InitLoggerModel model)
        {
            model.MinLevel = LogLevel.Debug;


           
        }






        class InitLoggerModel
        {
            public LogLevel MinLevel { get; set; }
            public string FileDiretoryPath { get; set; }
            public string FileNameTemplate { get; set; }

            public override int GetHashCode()
            {
                return this.MinLevel.GetHashCode() + this.FileDiretoryPath.GetHashCode() + this.FileNameTemplate.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                var b = obj as InitLoggerModel;
                if (b == null)
                    return false;
                return this.MinLevel == b.MinLevel && this.FileDiretoryPath == b.FileDiretoryPath && this.FileNameTemplate == b.FileNameTemplate;
            }
        }










        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}