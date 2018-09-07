using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 批发订单状态
    /// </summary>
    /// <remarks>订单状态从 1开始到 30</remarks>
    public enum WholesaleOrderState
    {
        #region 订单状态

        全部订单 = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        已下单 = 1,

        /// <summary>
        /// 用户取消订单
        /// </summary>
        已取消 = 2,

        /// <summary>
        /// 审核通过
        /// </summary>
        审核通过 = 3,

        /// <summary>
        /// 审核通过
        /// </summary>
        审核不通过 = 4,

        /// <summary>
        /// 已分配配送人员
        /// </summary>
        待取货 = 5,

        /// <summary>
        /// 已发货
        /// </summary>
        已发货 = 6,

        /// <summary>
        /// 已完成
        /// </summary>
        已完成 = 7,

        /// <summary>
        /// 配送失败
        /// </summary>
        配送失败 = 8,

        /// <summary>
        /// 配送失败入库
        /// </summary>
        已入库 = 9,

        /// <summary>
        /// 退货
        /// </summary>
        已退货 = 10,

        #endregion

    }
}
