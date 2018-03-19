
using System;

namespace com.mh.model.messages
{
    [Flags]
    public enum MessageAction
    {
        CreateEntity = 1,
        UpdateEntity = 2,
        DeleteEntity = 4,
        Paid = 8,
        Approved = 16,
        SendPhone = 32,
        Favorite = 64,
        ChatToBuyer = 128,
        TouchBuyer = 256,
        SendRedPack = 512,
        ConfirmGoods = 1024,
        DeliverGoods = 1025,
        ResourceAsync = 2048,
        ChatToCustomer = 4096,


        Statistic = 5000,
        WxPayQuery = 5001,
        Subscribe = 5002,
        RmaAction = 5003,
        ProductAccount = 5004,
        /// <summary>
        /// 导出数据
        /// </summary>
        Export = 5005,

        /// <summary>
        /// 发送邮件
        /// </summary>
        SendEmail = 5006,

        /// <summary>
        /// 扣减库存
        /// </summary>
        InventorySub = 5501,

        /// <summary>
        /// 发放优惠券消息
        /// </summary>
        Coupon = 5502,

        /// <summary>
        /// 改价
        /// </summary>
        ChangePrice = 5503,

        /// <summary>
        /// 活动优惠券转化率
        /// </summary>
        ActionCouponConversionRate = 5504,

        #region ElasticSearch同步数据
        /// <summary>
        /// 同步商品
        /// </summary>
        ESSync = 6000,
        #endregion

        #region Es 统计数据
        /// <summary>
        /// 统计数据
        /// </summary>
        ESSyncStat = 6001,

        /// <summary>
        /// 用户访问资源统计
        /// </summary>
        ESUserVisitResource = 6002,
        #endregion

        /// <summary>
        /// 同步Mongo数据
        /// </summary>
        ChatToMongo = 6100,

        /// <summary>
        /// 圈子异步操作
        /// </summary>
        ChatForAsync = 6101,


        #region 排行榜
        /// <summary>
        /// 活力排行统计
        /// </summary>
        Rank = 7000,

        #endregion
        //极光推送
        JPush = 10000,

        /// <summary>
        /// 群发消息
        /// </summary>
        MassMessage = 11000,

        /// <summary>
        /// 发送系统消息
        /// </summary>
        SystemMessage = 11100,

        /// <summary>
        /// 微信模板消息
        /// </summary>
        WxTemplateMsg = 12000,

        #region 策略
        StrategyForFirstOrder = 13000,

        #endregion



        ScanBuyer = 14000,
        NewFavorite = 14001,



        #region MemberInfo

        MemberInfo = 15000,
        #endregion


        ShortMessage = 16000,


        /// <summary>
        /// 15min优惠券导入
        /// </summary>
        CouponImport = 17000,

        /// <summary>
        /// UDMP 门店导入
        /// </summary>
        SectionImport = 17002,


        /// <summary>
        /// 15min员工特权券导入
        /// </summary>
        PrivilegeCouponImport = 17001,

        /// <summary>
        /// 组织架构变化
        /// </summary>
        OrgChange = 17003,

        /// <summary>
        /// 会员分组导入
        /// </summary>
        MemberGroupImport = 17005,
        /// <summary>
        /// UDMP 门店导入
        /// </summary>
        ImsOperatorImport = 17006,


        /// <summary>
        /// 微信事件日志
        /// </summary>
        WxEventLog = 18000,


        /// <summary>
        /// 组织架构变化:统计组
        /// </summary>
        OrgChangeForStat = 18001,
        /// <summary>
        /// 推送顾客信息
        /// </summary>
        SendUserInfoExport = 19000,

    }
}
