using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.PurchaseInStockModule
{
    /// <summary>
    /// 采购业务类型
    /// </summary>
    public enum PurchaseBusinessType
    {
        城市采购 = 0,
        大区联采 = 1,
        总部统采 = 2,
        城际调拨 = 3,
        代理采购 = 4,
    }
}
