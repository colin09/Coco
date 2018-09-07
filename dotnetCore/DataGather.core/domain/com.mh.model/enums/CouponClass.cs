using System.ComponentModel;

namespace com.mh.model.enums
{


    public enum CouponClass
    {
        /// <summary>
        /// 满减券
        /// </summary>
        [Description("满减券")]
        FullMinus = 0,

        /// <summary>
        /// 满折券
        /// </summary>
        [Description("打折券")]
        FullDiscount = 1,


        [Description("现金劵")]
        CashCoupon = 2,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知券")]
        Unknown = 99,

    }
}