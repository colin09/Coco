using System;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using System.Collections.Generic;

using com.mh.common.configuration;
using com.mh.common.extension;

namespace com.mh.dataengine.code
{

    public class RegistJobs
    {

        public static void InitJobs(IScheduler _scheduler)
        {

            var flag = true;
            var index = 0;
            do
            {
                var jobName = ConfigManager.Get($"Jobs:{index}:name");
                flag = !jobName.IsNull();

                if (flag)
                {
                    var type = ConfigManager.Get($"Jobs:{index}:type");
                    var cron_expression = ConfigManager.Get($"Jobs:{index}:cron-expression");
                    var data = GetJobData(index);

                    MkJobDetail(jobName, type, cron_expression, data, _scheduler);
                }

                index++;
            } while (flag);

        }

        private static Dictionary<string, string> GetJobData(int jobIndex)
        {
            var dic = new Dictionary<string, string>();
            var index = 0;
            var flag = true;
            do
            {
                var key = ConfigManager.Get($"Jobs:{jobIndex}:data:{index}:key");
                flag = !key.IsNull();
                if (flag)
                {
                    var val = ConfigManager.Get($"Jobs:{jobIndex}:data:{index}:val");
                    dic.Add(key, val);
                }
                index++;
            } while (flag);


            return dic;
        }


        private static async void MkJobDetail(string name, string type, string expression, Dictionary<string, string> data, IScheduler _scheduler)
        {
            var jobType = Type.GetType(type, true, true);
            JobBuilder builder = JobBuilder.Create(jobType)
                .WithIdentity(name, "Job_Group");
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    builder = builder.UsingJobData(item.Key, item.Value);
                }
            }
            IJobDetail job = builder.Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity($"{name}_Trigger", "Job_Group")
              //.StartNow()
              //.StartAt(DateBuilder.FutureDate(5, IntervalUnit.Minute)) // use DateBuilder to create a date in the future
              //.WithCronSchedule("0 0/2 8-17 * * ?")
              .WithCronSchedule(expression)
              //.WithSimpleSchedule(x => x.WithIntervalInSeconds(40).RepeatForever())
              //.EndAt(DateBuilder.DateOf(22, 0, 0)) //22:00:00
              //.ForJob(myJobKey)
              .Build();

            await _scheduler.ScheduleJob(job, trigger);
            System.Console.WriteLine($"job : {name} [{expression}] trigger !");
        }


    }

}