using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    public enum TakeStockAuditState
    {
        已取消 = -1,
        未开始=0,
        盘点中=1,
        待审核 = 2,
        审核通过 = 3,
        审核拒绝 = 4, 
    }
}
