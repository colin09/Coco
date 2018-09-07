using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    /// <summary>
    /// 销售业务类型
    /// </summary>
    public enum SaleBusinessType
    {
        城市销售 = 0,
        大区联采 = 1,
        总部统采 = 2,
        城际调拨 = 3,
        总部代理 = 4
    }
}
