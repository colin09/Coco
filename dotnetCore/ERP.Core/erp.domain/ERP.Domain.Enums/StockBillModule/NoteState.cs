using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    /// <summary>
    /// 清单状态
    /// </summary>
    public enum NoteState
    {
        待确认 = 1,
        已确认 = 2,
        已核算 = 9
    }
}
