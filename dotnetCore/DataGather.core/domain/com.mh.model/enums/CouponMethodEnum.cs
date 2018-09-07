

using System.ComponentModel;

namespace com.mh.model.enums
{
    /// <summary>
    /// 营销创建活动送券方式
    /// </summary>
    public enum CouponMethodEnum
    {
        //0:控制优惠力度，系统只能送券 1:给顾客发送同一种神马券 2:给顾客发送同一种15mins券
        /// <summary>
        /// 系统智能送券
        /// </summary>
        [Description("系统智能送券")]
        DiscountRate = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("神马券")]
        ShenMaCoupon = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("15mins券")]
        FFMinsCoupon = 2,

        /// <summary>
        /// 商户接入优惠券
        /// </summary>
        [Description("商户接入优惠券")]
        StoreAccessCoupon = 3,
    }




    
    //状态 -1 被删除 0：被淘汰 1：正常
    public enum OpcCouponConfigStatus
    {
        /// <summary>
        /// 删除
        /// </summary>
        Deleted = -1,

        /// <summary>
        /// 被淘汰
        /// </summary>
        KnockOut = 0,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
    }
}
