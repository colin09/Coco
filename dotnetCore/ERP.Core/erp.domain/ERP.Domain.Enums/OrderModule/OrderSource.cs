using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 订单来源
    /// </summary>
    public enum OrderSource
    {
        安卓客户端 = 1,

        苹果客户端 = 2,

        微信 = 3,

        商城 = 4,

        电话下单 = 5,

        定制酒 = 6,

        微信_附近终端 = 7,
    }
}
