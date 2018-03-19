


using com.mh.model.enums;
using com.mh.model.mongo.mgBase;
using MongoDB.Bson.Serialization.Attributes;

namespace com.mh.model.mongo.dbMh
{
    /// <summary>
    /// 策略执行
    /// </summary>
    [BsonIgnoreExtraElements]
    public class StoreExecute : MagicHorseBase
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// 输出配置
        /// </summary>
        public Output Output { get; set; }
        /// <summary>
        /// 优惠券来源配置
        /// </summary>
        public CouponSource CouponSource { get; set; }

        /// <summary>
        /// 发送消息时间
        /// </summary>
        public int PushMsgTimePoint { get; set; }



        public AppConfig AppConfig { set; get; }

        /// <summary>
        /// 短信息推送
        /// </summary>
        public ActionSmsConfig SmsConfig { get; set; }

        /// <summary>
        /// 门店下最多同时执行几个任务 包含扫码的
        /// </summary>
        public int MaxExecuteActionThreadCount { get; set; } = 3;

        /// <summary>
        /// 一个活动同时可以对多少顾客发券发消息
        /// </summary>
        public int MaxExecuteActionMemberInfoThreadCount { get; set; } = 10;
    }

    /// <summary>
    /// 执行输出
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Output
    {
        /// <summary>
        /// 插件Id
        /// </summary>
        public int ModuleId { get; set; }
        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态 -1 被删除 0：被淘汰 1：正常 
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 优惠券来源
    /// </summary>
    [BsonIgnoreExtraElements]
    public class CouponSource
    {
        public CouponSourceEnum Type { get; set; }
    }


    [BsonIgnoreExtraElements]
    public class AppConfig
    {
        public ContactConfig ContactConfig { set; get; }
        public CustomerRange CustomerRange { set; get; }
        public CustomerAllotRule CustomerAllotRule { set; get; }
        public OrderChangeCustomerRule OrderChangeCustomerRule { set; get; }
    }



    [BsonIgnoreExtraElements]
    public class ContactConfig
    {
        public bool IsChecked { set; get; }
        public bool IsCallMobile { set; get; }
        public bool IsCanSendMessage { set; get; }
        public bool IsOpenChat { set; get; }

    }

    [BsonIgnoreExtraElements]
    public class CustomerRange
    {
        public bool IsCheckedAllStoreCustomer { set; get; }
        public bool IsCheckedBuyerCustomer { set; get; }
    }


    [BsonIgnoreExtraElements]
    public class CustomerAllotRule
    {
        public bool ISStoreManagerAllot { set; get; }
        public bool IsBuyerCanGet { set; get; }
    }


    [BsonIgnoreExtraElements]
    public class OrderChangeCustomerRule
    {
        public bool IsNullChecked { set; get; }
        public bool IsFirstOrder { set; get; }
        public bool IsLastOrder { set; get; }
    }


    [BsonIgnoreExtraElements]
    public class ActionSmsConfig
    {
        /// <summary>
        /// 是否打开短信功能
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 短信签名位置  0：前面  1：后面
        /// </summary>
        public int SignPosition { get; set; }

        /// <summary>
        /// 活动短信推送插件Id
        /// </summary>
        public int ModuleId { get; set; }

    }
}
