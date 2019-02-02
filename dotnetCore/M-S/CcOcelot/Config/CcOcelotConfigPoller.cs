using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.Repository;
using Ocelot.Logging;

namespace CcOcelot {
    public class CcOcelotConfigPoller : IHostedService, IDisposable {
        private Timer _timer;
        private bool _polling;
        private readonly CcOcelotConfig _config;

        private readonly IOcelotLogger _logger;
        private readonly IFileConfigurationRepository _configRepository;
        private readonly IInternalConfigurationRepository _internalConfigRepository;
        private readonly IInternalConfigurationCreator _internalConfigCreator;

        public CcOcelotConfigPoller (CcOcelotConfig config,
            IOcelotLoggerFactory loggerFactory,
            IFileConfigurationRepository configRepository,
            IInternalConfigurationCreator internalConfigCreator,
            IInternalConfigurationRepository internalConfigRepository
        ) {
            _config = config;
            _configRepository = configRepository;
            _internalConfigCreator = internalConfigCreator;
            _internalConfigRepository = internalConfigRepository;
            _logger = loggerFactory.CreateLogger<CcOcelotConfigPoller> ();
        }

        public void Dispose () {
            _timer.Dispose ();
        }

        public Task StartAsync (CancellationToken cancellationToken) {
            if (_config.EnableTimer) {

            }
            return Task.CompletedTask;
        }

        public Task StopAsync (CancellationToken cancellationToken) {
            if (_config.EnableTimer) {
                _timer?.Change(Timeout.Infinite,0);
            }
            return Task.CompletedTask;
        }
    }
}