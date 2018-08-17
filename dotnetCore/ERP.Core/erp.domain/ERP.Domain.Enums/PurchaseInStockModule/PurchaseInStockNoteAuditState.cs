using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.PurchaseInStockModule
{
    public enum PurchaseInStockNoteAuditState
    {
        待采购审核 = 1,
        待财务审核 = 2,
        采购拒绝 = 3,
        财务拒绝 = 4,
        异常审核 = 5,
        异常拒绝 = 6,
        已核算 = 9,
    }
}
