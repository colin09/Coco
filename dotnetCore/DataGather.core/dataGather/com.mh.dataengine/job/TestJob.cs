using System;
using Quartz;
using System.Threading.Tasks;

namespace com.mh.dataengine.job
{

    class TestJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            var store = dataMap.GetString("store");
            var section = dataMap.GetString("section");

            await System.Console.Out.WriteLineAsync($"{DateTime.Now} from [TestJob] , key={key}, store={store}, section={section}");
        }


    }
}
