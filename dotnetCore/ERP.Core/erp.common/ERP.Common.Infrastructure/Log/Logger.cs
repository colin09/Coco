using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace ERP.Common.Infrastructure.Log {

    public class Logger : ILogger {

        private static ILog _log;
        private static Logger _current = null;

        private Logger (string name, FileInfo fileInfo) {
            var repository = LogManager.CreateRepository (Assembly.GetEntryAssembly (), typeof (log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure (repository, fileInfo);
            _log = LogManager.GetLogger (repository.Name, name);
        }
        private Logger (string name) {
            var repository = LogManager.CreateRepository (Assembly.GetEntryAssembly (), typeof (log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure (repository, new FileInfo ("configurations/log4net.config"));
            _log = LogManager.GetLogger (repository.Name, name);

        }
        private Logger () : this ("") { }

        public static Logger Current () {
            if (_current == null) {
                _current = new Logger ();
            }
            return _current;
        }

        /* Log a message object */
        public void Debug (object message) {
            _log.Info (message);
        }
        public void Info (object message) {
            _log.Info (message);
        }
        public void Warn (object message) {
            _log.Info (message);
        }
        public void Error (object message) {
            _log.Error (message);
        }
        public void Fatal (object message) {
            _log.Error (message);
        }

    }

}