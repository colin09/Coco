using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.mq.client;
using com.wx.mq.message;

namespace com.wx.consumer
{
    public partial class Service1 : ServiceBase
    {

        readonly ILog log;

        public Service1()
        {
            InitializeComponent();
            UnityBootStrapper.Init();
            log = LocatorFacade.Current.Resolve<ILog>();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("Service.OnStart");

            ListenInit();
        }

        protected override void OnStop()
        {
            log.Info("Service.OnStop");
        }


        public void ListenInit()
        {
            foreach (var item in Enum.GetValues(typeof(MqEventType)))
            {
                try
                {
                    Task.Factory.StartNew(() =>
                    {
                        MqClient client = new MqClient();
                        client.ListenEventMessage(routtingKey: new string[] { item.ToString() });
                    });
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
            }
        }

        protected void SendMq()
        {
            MqClient client = new MqClient();
            foreach (var item in Enum.GetValues(typeof(MqEventType)))
            {
                var msg = new MqEventMessage()
                {
                    Id = DateTime.Now.Millisecond,
                    IsOperationOK = false,
                    EventType = (MqEventType)item,
                    EventKey = "users",
                    EventSource = DateTime.Now.DayOfYear.ToString()
                };

                client.TriggerEventMessage(msg);
            }
        }




    }
}
