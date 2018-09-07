

namespace com.mh.model.messages
{
    public enum MessageSourceType
    {
        Product = 1,
        Combo = 2,
        Inventory = 3,
        OrderPaid = 4,
        GiftCard = 5,
        SectionBrand = 6,
        DaogouApply = 7,
        SendPhone = 8,
        Favorite = 9,
        ChatToBuyer = 10,
        TouchBuyer = 11,
        SendRedPack = 12,
        ConfirmGoods = 13,
        ResourceAsync = 14,
        ChatToCustomer = 15,
        Statistic = 16,
        WxPayQuery = 17,
        Subscribe = 18,
        RmaAction = 19,
        ProductAccount = 20,
        /// <summary>
        /// 导出数据
        /// </summary>
        Export = 21,
        DeliverGoods = 22,

        InventorySub = 30,

        /// <summary>
        /// 发放优惠券
        /// </summary>
        Coupon = 31,

        /// <summary>
        /// 改价
        /// </summary>
        ChangePrice = 32,

        /// <summary>
        /// 活动优惠券转化率
        /// </summary>
        ActionCouponConversionRate = 33,

        #region Elasticsearch同步数据
        /// <summary>
        /// 同步商品数据
        /// </summary>
        ESSync = 600,
        #endregion

        /// <summary>
        /// 统计数据
        /// </summary>
        ESSyncStat = 700,

        /// <summary>
        /// 用户访问资源统计
        /// </summary>
        ESUserVisitResource = 701,

        /// <summary>
        /// 同步Mongo数据
        /// </summary>
        ChatToMongo = 750,

        /// <summary>
        /// 圈子异步操作
        /// </summary>
        ChatForAsync = 751,

        /// <summary>
        /// 极光推送
        /// </summary>
        JPush = 100,

        /// <summary>
        /// 群发消息
        /// </summary>
        MassMessage = 110,

        /// <summary>
        /// 发送系统消息
        /// </summary>
        SystemMessage = 111,

        /// <summary>
        /// 微信模板消息
        /// </summary>
        WxTemplateMsg = 120,

        #region 策略
        StrategyForFirstOrder = 130,

        #endregion

        ScanBuyer=140,

        /// <summary>
        /// 排行统计
        /// </summary>
        Rank = 200,


        #region 执行
        WxEventTextExecute =300,
        WxScanMQExecute=301,
        /// <summary>
        /// 微信事件处理
        /// </summary>
        WxEventExecute=303,
        #endregion


        #region MemberInfo
        MemberInfo=400,
        #endregion

        ShortMessage=500,

        /// <summary>
        /// 活动发送短信
        /// </summary>
        ActionSendSmsMessage=501,

        /// <summary>
        /// 15mins优惠券导入
        /// </summary>
        CouponImport=502,

        /// <summary>
        /// 活动预览
        /// </summary>
        ActionPreview=503,

        /// <summary>
        /// 门店导入
        /// </summary>
        SectionImport = 505,
        /// <summary>
        /// 15mins员工特权优惠券导入
        /// </summary>
        PrivilegeCouponImport = 504,
        
        /// <summary>
        /// 组织架构变化
        /// </summary>
        OrgChange=506,


        /// <summary>
        /// 会员分组导入
        /// </summary>
        MemberGroupImport = 507,
        /// <summary>
        /// 门店导入
        /// </summary>
        ImsOperatorImport = 508,


        /// <summary>
        /// 微信事件日志
        /// </summary>
        WxEventLog=600,

        /// <summary>
        /// 组织架构变化:统计组
        /// </summary>
        OrgChangeForStat = 801,
        /// <summary>
        /// 推送顾客信息
        /// </summary>
        SendUserInfoExport=901,
    }
}
