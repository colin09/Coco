using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayType
    {
        货到付款 = 0,
        微信支付 = 1,
        支付宝支付 = 2,
        银联支付 = 3,
        其他支付 = 4
        //在线支付 = 1,
        //积分兑换 = 2,
    }
}
