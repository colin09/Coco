using System.Threading.Tasks;

using com.mh.common.ioc;
using com.mh.rabbit.iservice;
using com.mh.model.messages.message;
using com.mh.model.messages.datagatherMessage;
using com.mh.model.messages;

namespace com.mh.rabbit
{
    public static class MessagelistenerClient
    {

        private static readonly IMessageSender<BaseMessage> Sender = IocProvider.GetService<IMessageProvider<BaseMessage>>().GetSender();
        private static readonly IMessageSender<DataGatherBaseMessage> DatagatherSender = IocProvider.GetService<IMessageProvider<DataGatherBaseMessage>>().GetSender();



        #region  --  message send  --

        public static Task<bool> SyncMemberInfo(MemberInfoSyncType type, MemberInfoDto memberInfo)
        {
            return Task.Run(() =>
            {
                var message = new MemberInfoMessage()
                {
                    SyncType = type,
                    MemberInfo = memberInfo,
                };
                var rel = Sender.SendMessageReliable(message);
                return rel;
            });
        }


        public static Task<bool> SyncMemberInfoByMemberId(string memberId)
        {
            return Task.Run(() =>
            {
                var message = new ESSyncMessage()
                {
                    SourceType = (int)MessageSourceType.ESSync,
                    SyncType = SyncType.SyncMemberInfo,
                    ESSyncMemberInfo = new ESSyncMemberInfoRequest
                    {
                        MemberId = memberId
                    }
                };
                //var rel = Sender.SendMessageReliable(message);
                //return rel;
                return true;
            });
        }

        public static Task<bool> SyncMemberInfoByManagerId(string managerId)
        {
            return Task.Run(() =>
            {
                var message = new ESSyncMessage()
                {
                    SourceType = (int)MessageSourceType.ESSync,
                    SyncType = SyncType.SyncMemberInfo,
                    ESSyncMemberInfo = new ESSyncMemberInfoRequest
                    {
                        ManagerId = managerId
                    }
                };
                //var rel = Sender.SendMessageReliable(message);
                //return rel;
                return true;
            });
        }
        #endregion



        #region  --  datagather message send  --


        #endregion







    }
}