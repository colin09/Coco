using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.FinancialStockModule
{
    public enum FinancialStockType
    {
        期初结存 = 0,

        采购入库单 = 1,
        销售出库单 = 2,
        退货入库单 = 21,
        采购退货单 = 11,
        物料调拨单 = 12,

        盘盈单 = 3,
        盘亏单 = 4,

        破损出库 = 5,
        招待 = 51,
        福利 = 52,
        客情 = 53,

        兑奖出库 = 6,
        兑奖入库 = 7,
        成本调整单 = 8,
        期末结存 = 9,
        兑奖入库单 = 10,
    }
}
