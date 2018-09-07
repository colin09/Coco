using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 退货单状态
    /// </summary>
    public enum ReturnState
    {
        #region 退货状态

        无 = 0,

        /// <summary>
        /// 用户申请退货
        /// </summary>
        申请退货 = 1,

        /// <summary>
        /// 退货单已关闭
        /// </summary>
        已取消退货 = 2,

        /// <summary>
        /// 运营中心审核通过
        /// </summary>
        区域审核通过 = 3,

        /// <summary>
        /// 运营中心审核拒绝
        /// </summary>
        区域审核拒绝 = 4,

        /// <summary>
        /// 运营中心审核通过
        /// </summary>
        运营审核通过 = 5,

        /// <summary>
        /// 运营中心审核拒绝
        /// </summary>
        运营审核拒绝 = 6,

        /// <summary>
        /// 取货中
        /// </summary>
        待取货 = 7,

        /// <summary>
        /// 酒厂已退货
        /// </summary>
        已取货 = 8,

        /// <summary>
        /// 退货单已关闭
        /// </summary>
        拒绝取货 = 9,

        /// <summary>
        /// 退货入库
        /// </summary>
        已退货 = 10


        #endregion
    }
}
