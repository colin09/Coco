using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.FinancialDailyModule
{
    public enum DailyType
    {
        货到付款现金 = 1,
        货到付款银行卡 = 2,
        微信 = 3,
        支付宝 = 4,

        预收款 = 11,
        预付款 = 12,
        预收退款 = 13,
        预付退款 = 14,

        期初应收 = 21,
        期初应付 = 22,

    }
}
