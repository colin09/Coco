using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 批发订单用户状态
    /// </summary>
    public enum WholesaleOrderUserState
    {
        全部订单 = 0,

        待发货 = 1,

        已发货 = 2,

        已完成 = 3,

        审核拒绝 = 4,

        已取消 = 5,

        已删除 = 6,
        配送失败 = 7,
    }
}
