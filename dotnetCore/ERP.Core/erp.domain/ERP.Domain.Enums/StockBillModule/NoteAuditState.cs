using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    /// <summary>
    /// 单据审核状态(待审核 = 1,审核通过 = 2,审核拒绝 = 3,已核算 = 9,)
    /// </summary>
    public enum NoteAuditState
    {
        //无需审核 = 0,
        待审核 = 1,
        审核通过 = 2,
        审核拒绝 = 3,

        已核算 = 9,
    }
}
