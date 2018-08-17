using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.AllocationNoteModule
{
    public enum AllocationState
    {
        /// <summary>
        /// 只有城际调拨需要确认
        /// </summary>
        待确认 = 0,
        待发货 = 1,
        收货中 = 2,
        待结算 = 3,
        已结算 = 4,
        /// <summary>
        /// 只有城际调拨可拒绝
        /// </summary>
        已拒绝 = 8,
        已取消 = 9,
    }
}
