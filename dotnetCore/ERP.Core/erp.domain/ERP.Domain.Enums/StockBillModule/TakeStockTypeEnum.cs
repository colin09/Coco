using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    /// <summary>
    /// 盘点单类型(普通盘点=1,每周动盘=2)
    /// </summary>
    public enum TakeStockTypeEnum
    {
        普通盘点 = 1,
        每周动盘 = 2,
        月末盘点 = 3,
        整仓盘点 = 4,
        强制盘点 = 5
    }
}
