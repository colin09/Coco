using System;
using com.mh.common.configuration;

namespace com.mh.model.messages.datagatherMessage
{
    public class DataGatherBaseMessage : IBaseMessage
    {

        private long _messageId = DateTime.UtcNow.Ticks;
        public virtual long MessageId
        {
            get { return _messageId; }
            set { _messageId = value; }
        }

        public virtual int ActionType { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName
        {
            get
            {
                var name = this.GetType().Name.ToLower();
                if (name.LastIndexOf("message") > -1)
                {
                    name = name.Remove(name.Length - "message".Length);
                }
                return string.Format("{0}_{1}", ConfigManager.RabbitDataGatherQuenuName, name).ToLower();

            }
        }

        /// <summary>
        /// 失败队列名称
        /// </summary>
        public string FailQueueName { get { return string.Format("{0}_{1}", QueueName, "fail").ToLower(); } }

    }
}
