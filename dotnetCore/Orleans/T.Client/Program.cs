using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using T.IGrains;

namespace T.Client {
    class Program {
        static int  Main (string[] args) {
            Console.WriteLine ("start , runing ");

            return RunAsync ().Result;
        }

        private static async Task<int> RunAsync () {
            try {
                using (var client = await StartClientWithRetries ()) {
                    var gV = client.GetGrain<IValue> (0);
                    var result = await gV.Say ("w is coming...");
                    Console.ReadKey ();
                }

                return 0;
            } catch (Exception e) {
                Console.WriteLine (e);
                Console.ReadKey ();
                return 1;
            }
        }

        private static int attempt = 0;
        const int initializeAttemptsBeforeFailing = 5;
        private static async Task<IClusterClient> StartClientWithRetries () {
            attempt = 0;
            IClusterClient client;
            client = new ClientBuilder ()
                .UseLocalhostClustering ()
                .Configure<ClusterOptions> (options => {
                    options.ClusterId = "dev";
                    options.ServiceId = "T.Grains";
                })
                .ConfigureLogging (logging => logging.AddConsole ())
                .Build ();

            await client.Connect (RetryFilter);
            Console.WriteLine ("Client successfully connect to silo host");
            return client;
        }

        private static async Task<bool> RetryFilter (Exception exception) {
            if (exception.GetType () != typeof (SiloUnavailableException)) {
                Console.WriteLine ($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine ($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing) {
                return false;
            }
            await Task.Delay (TimeSpan.FromSeconds (4));
            return true;
        }

    }
}