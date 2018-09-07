using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.ShopModule
{
    public enum ShopPurchaseInStockNoteState
    {
        申请入库 = 0,
        待入库 = 1,
        部分入库 = 2,
        拒绝入库 = 3,
        已入库 = 4,
        已过期 = 5,
    }
}
