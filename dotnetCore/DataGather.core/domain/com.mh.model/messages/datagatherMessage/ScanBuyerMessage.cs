
using System;

namespace com.mh.model.messages.datagatherMessage
{
    public class ScanBuyerMessage : DataGatherBaseMessage
    {

        public override int ActionType
        {
            get { return (int)MessageAction.ScanBuyer; }
        }

        public int StoreId { get; set; }
        public int SectionId { set; get; }
        public int CustomerUserId { get; set; }
        public int BuyerUserId { get; set; }
        /// <summary>
        /// 是否送券
        /// </summary>
        public int SendCoupon { set; get; }
        public DateTime ScanTime { set; get; }

        public int FromPlatform { set; get; }



        public string AppId { set; get; }
        public string CustomerOpenId { set; get; }

    }
}