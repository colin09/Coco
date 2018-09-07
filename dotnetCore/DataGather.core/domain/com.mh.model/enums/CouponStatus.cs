using System.ComponentModel;

namespace com.mh.model.enums
{
    public enum CouponStatus
    {
        /// <summary>
        /// 锁定
        /// </summary>
        Locked = -10,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete = -1,
        /// <summary>
        /// 失效
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 未使用
        /// </summary>
        Available = 1,
        /// <summary>
        /// 已发送
        /// </summary>
        Sent = 2,
        /// <summary>
        /// 已使用
        /// </summary>
        Used = 3,


    }
}