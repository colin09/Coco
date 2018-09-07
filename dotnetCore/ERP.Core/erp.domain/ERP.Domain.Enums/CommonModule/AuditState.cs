using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.CommonModule
{
    /// <summary>
    /// 单据状态
    /// </summary>
    public enum AuditState
    {
        待审核 = 1,
        已审核 = 2,
        审核不通过 = 3,
    }
}
