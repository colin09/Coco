using ERP.Domain.Model.FinancialBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentSettlementReceivableBillItem : BaseEntity
    {
        /// <summary>
        /// 核算金额
        /// </summary>
        public decimal SettlementAmount { get; set; }

        /// <summary>
        /// 核算数量
        /// </summary>
        public int SettlementCount { get; set; }

        [ForeignKey("ReceivableBillItem")]
        [MaxLength(64)]
        public string ReceivableBillItem_Id { get; set; }
        public ReceivableBillItem ReceivableBillItem { get; set; }

        [ForeignKey("AgentSettlementNoteProductInfoItem")]
        [MaxLength(64)]
        public string AgentSettlementNoteProductInfoItem_Id { get; set; }

        public AgentSettlementNoteProductItem AgentSettlementNoteProductInfoItem { get; set; }

        [ForeignKey("AgentSettlementNote")]
        [MaxLength(64)]
        public string AgentSettlementNote_Id { get; set; }
        public AgentSettlementNote AgentSettlementNote { get; set; }
    }
}
