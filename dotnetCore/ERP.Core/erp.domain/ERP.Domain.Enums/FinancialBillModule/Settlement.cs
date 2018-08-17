using ERP.Domain.Enums.FinancialDailyModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.FinancialBillModule
{
    /// <summary>
    /// 结算方式
    /// </summary>
    public enum Settlement
    {
        货到付款现金 = 1,
        货到付款银行卡 = 2,
        微信 = 3,
        支付宝 = 4,
    }

    public static class SettlementExtensions
    {
        public static DailyType ToDailyType(this Settlement settlement)
        {
            return (DailyType)(int)settlement;
        }
    }
}
