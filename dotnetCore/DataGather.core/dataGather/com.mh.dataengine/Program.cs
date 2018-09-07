using System;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

using com.mh.dataengine.job;
using com.mh.dataengine.code;
using com.mh.common.configuration;

namespace com.mh.dataengine
{
    class Program
    {
        private static IScheduler _scheduler = null;

        static void Main(string[] args)
        {
            try
            {
                BootStrapper.Init() ;

                while (true)
                {
                    var key = "";
                    if (args.Length > 0)
                    {
                        key = args[0];
                        args = new string[0];
                    }
                    else key = Console.ReadLine();
                    Console.WriteLine("Press the key, q/Q for quit");

                    switch (key.ToLower())
                    {
                        case "all job": OnStart(); break;
                        case "start job": StartJobs().GetAwaiter().GetResult(); break;
                        case "q": return;
                        default: System.Console.WriteLine("wrong key"); break;
                    }
                }
            }
            finally
            {
                OnStop();
            }

        }


        private static void OnStart()
        {
            try
            {
                // trigger async evaluation
                StartJobs().GetAwaiter().GetResult();


            }
            catch (SchedulerException ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        private static void OnStop()
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                _scheduler.Shutdown();
                System.Console.WriteLine("shut down scheduler...");
            }
        }





        private static async Task StartJobs()
        {
            if (_scheduler != null && _scheduler.IsStarted)
                return;

            //StdSchedulerFactory factory = new StdSchedulerFactory(props);
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = await factory.GetScheduler();

            // and start it off
            await _scheduler.Start();
            System.Console.WriteLine("job started...");

            RegistJobs.InitJobs(_scheduler);            
        }
        private static async Task StopJobs()
        {
            await _scheduler.Shutdown();
        }


    }
}
