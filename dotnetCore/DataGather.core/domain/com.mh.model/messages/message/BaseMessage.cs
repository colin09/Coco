using System;
using com.mh.common.configuration;

namespace com.mh.model.messages.message
{
    public class BaseMessage : IBaseMessage
    {
        private long _messageId = DateTime.UtcNow.Ticks;
        public virtual long MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                _messageId = value;
            }
        }
        public int EntityId { get; set; }

        public virtual int ActionType { get; set; }

        public virtual int SourceType { get; set; }

        public string SourceNo { get; set; }

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
                return $"{ConfigManager.RabbitQuenuName}_{name}".ToLower();

            }
        }

        /// <summary>
        /// 失败队列名称
        /// </summary>
        public string FailQueueName { get { return string.Format("{0}_{1}", QueueName, "fail").ToLower(); } }


    }
}
