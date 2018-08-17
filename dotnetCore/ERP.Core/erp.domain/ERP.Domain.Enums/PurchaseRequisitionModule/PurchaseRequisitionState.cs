using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.PurchaseRequisitionModule
{
    public enum PurchaseRequisitionState
    {
        未提交 = 0,
        审核中 = 1,
        交货中 = 2,
        已拒绝 = 3,
        已完成 = 4,
        已过期 = 5,
        已取消 = 6,
    }
}
