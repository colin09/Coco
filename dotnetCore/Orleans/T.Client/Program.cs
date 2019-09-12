using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using T.IGrains;

namespace T.Client {
    class Program {
        static int Main (string[] args) {
            Console.WriteLine ("start , runing ");

            Task.Run (() => StartClientWithRetries ());

            System.Console.ReadLine ();
            return 0;
        }

        private static int attempt = 0;
        const int initializeAttemptsBeforeFailing = 5;
        private static async Task<IClusterClient> StartClientWithRetries () {
            attempt = 0;

            var gateways = new IPEndPoint[] {
                // new IPEndPoint (IPAddress.Parse ("197.168.25.234"), 50000),
                // new IPEndPoint (IPAddress.Parse ("197.168.25.234"), 8300),
                new IPEndPoint (IPAddress.Parse ("197.168.25.234"), 32222),
            };
            IClusterClient client;
            client = new ClientBuilder ()
                // .UseLocalhostClustering ()
                .UseStaticClustering (gateways)
                // .UseAdoNetClustering (options => {
                //     options.Invariant = "System.Data.SqlClient";
                //     options.ConnectionString = "Server=172.16.1.118;Database=OrleansStorage;User ID=user;Password=123456;Trusted_Connection=false";
                // })
                .Configure<ClusterOptions> (options => {
                    options.ClusterId = "yjp.erp.test";
                    options.ServiceId = "T.Grains";
                })
                .ConfigureLogging (logging => logging.AddConsole ())
                .Build ();

            await client.Connect (RetryFilter);
            Console.WriteLine ("################################ Client successfully connect to silo host ################################ ");
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