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

        static void WaitExit () {
            var flag = true;
            while (flag) {
                var key = System.Console.ReadLine ();
                if (key != null && key.ToLower () == "exit")
                    flag = false;
                // System.Console.WriteLine ("Enter 'exit' to exit ...");
            }
        }

        private static async Task<int> RunAsync () {
            try {
                var host = await StartSilo ();
                Console.WriteLine ("Press Enter to terminate...");
                WaitExit ();

                await host.StopAsync ();
                System.Console.WriteLine ("#####################################################################################################################");
                System.Console.WriteLine ("####### host started ################################################################################################");
                System.Console.WriteLine ("#####################################################################################################################");
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
                    // options.SiloPort = 0;
                    // options.GatewayPort = 0;
                    // options.SiloListeningEndpoint = new IPEndPoint (IPAddress.Loopback, 0);
                    // options.GatewayListeningEndpoint = new IPEndPoint (IPAddress.Loopback, 0);
                })
                .ConfigureApplicationParts (parts => parts.AddApplicationPart (typeof (T.Grains.Value).Assembly).WithReferences ())
                .ConfigureLogging (logging => logging.AddConsole ());

            var host = builder.Build ();
            await host.StartAsync ();
            return host;
        }
    }
}