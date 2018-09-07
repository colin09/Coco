using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 配送商订单状态
    /// </summary>
    /// <remarks>订单状态从 1开始到 30</remarks>
    public enum SupplierOrderState
    {
        #region 订单状态

        /// <summary>
        /// 全部订单
        /// </summary>
        全部 = 0,

        /// <summary>
        /// 新订单
        /// </summary>
        已下单 = 1,

        /// <summary>
        /// 用户取消订单
        /// </summary>
        已取消 = 2,

        审核通过 = 3,

        审核不通过 = 4,

        /// <summary>
        /// 已拒绝
        /// </summary>
        配送商拒绝 = 5,

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
        已转到酒批 = 9,
        #endregion

    }
}
