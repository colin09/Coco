using System.ComponentModel;

namespace com.mh.model.enums
{

    /// <summary>
    /// 营销策略执行：优惠券来源
    /// </summary>
    public enum CouponSourceEnum
    {
        /// <summary>
        /// Oss Excel导入
        /// </summary>
        [Description("null")]
        NullCoupon = -1,

        /// <summary>
        /// 系统智能送券   0,1
        /// </summary>
        [Description("系统智能送券")]
        DiscountRate = 0,

        /// <summary>
        /// 神马规则优惠券 
        /// </summary>
        [Description("神马规则优惠券")]
        ShenMaCoupon = 1,

        /// <summary>
        /// 神马平台导入
        /// </summary>
        [Description("神马平台导入")]
        PlatformImport = 2,

        /// <summary>
        /// 商户接入优惠券
        /// </summary>
        [Description("商户接入优惠券")]
        StoreAccessCoupon = 3,
    }
}