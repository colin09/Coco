using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using T.Grains;

namespace T.Host {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");

            var r = RunAsync ().Result;
        }
        private static async Task<int> RunAsync () {
            try {
                var host = await StartSilo ();
                Console.WriteLine ("Press Enter to terminate...");
                Console.ReadLine ();

                await host.StopAsync ();

                return 0;
            } catch (Exception ex) {
                Console.WriteLine (ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo () {
            // define the cluster configuration
            var builder = new SiloHostBuilder ()
                .UseLocalhostClustering ()
                .Configure<ClusterOptions> (options => {
                    options.ClusterId = "dev";
                    options.ServiceId = "T.Grains";
                })
                .Configure<EndpointOptions> (options => {
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.SiloPort = 0;
                    options.GatewayPort = 0;
                    options.SiloListeningEndpoint = new IPEndPoint (IPAddress.Loopback, 0);
                    options.GatewayListeningEndpoint = new IPEndPoint (IPAddress.Loopback, 0);
                })
                .ConfigureApplicationParts (parts => parts.AddApplicationPart (typeof (T.Grains.Value).Assembly).WithReferences ())
                .ConfigureLogging (logging => logging.AddConsole ());

            var host = builder.Build ();
            await host.StartAsync ();
            return host;
        }
    }
}