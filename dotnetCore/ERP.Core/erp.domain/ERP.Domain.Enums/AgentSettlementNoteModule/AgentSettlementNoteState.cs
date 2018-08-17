using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.AgentSettlementNoteModule
{
    public enum AgentSettlementNoteState
    {
        待提交 = 0,
        待核算 = 1,
        已付款 = 2,
        已核算 = 3,
    }
}
