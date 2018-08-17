using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.AuditTraceModule
{
    public enum AuditTraceState
    {
        发起申请 = 0,
        待审核 = 1,
        审核通过 = 2,
        审核拒绝 = 3,
        已取消 = 4,
        已转交 = 5,
        评论 = 6,
    }
}
