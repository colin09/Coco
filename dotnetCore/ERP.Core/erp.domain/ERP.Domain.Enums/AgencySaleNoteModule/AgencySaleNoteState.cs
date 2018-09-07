using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.AgencySaleNoteModule
{
    /// <summary>
    /// 代销售单据状态
    /// </summary>
    public enum AgencySaleNoteState
    {
        待确认 = 0,
        待确认收货 = 1,
        待确认收款 = 2,
        已确认收货 = 3,
        已确认收款 = 4
    }
}
