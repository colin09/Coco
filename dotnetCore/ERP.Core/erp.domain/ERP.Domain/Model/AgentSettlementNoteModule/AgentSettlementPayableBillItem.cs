using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentSettlementPayableBillItem : BaseEntity
    {
        [ForeignKey("PayableBillItem")]
        [MaxLength(64)]
        public string PayableBillItem_Id { get; set; }
        public PayableBillItem PayableBillItem { get; set; }

        public decimal SettlementAmount { get; set; }

        public int SettlementCount { get; set; }

        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }

        public Product Product { get; set; }

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
