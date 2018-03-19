using System.ComponentModel;

namespace com.mh.model.enums
{

    /// <summary>
    /// 会员级别变化通知类型
    /// </summary>
    public enum VipLeaveNoticType
    {
        /// <summary>
        /// 按照注册时间
        /// </summary>
        [Description("注册时间")]
        ByRegisterTime = 0,
        /// <summary>
        /// 按照第一次购买时间
        /// </summary>
        [Description("首购时间")]
        ByFirstBuyTime = 1,
        /// <summary>
        /// 按照会员级别变化时间
        /// </summary>
        [Description("成为当前级别的日期")]
        ByVipLeveChangeTime = 2
    }
}