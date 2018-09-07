using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.SettlementNoteItemModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentSettlementNoteItem : BaseEntity
    {
        [ForeignKey("SettlementNoteItem")]
        [MaxLength(64)]
        public string SettlementNoteItem_Id { get; set; }
        public SettlementNoteItem SettlementNoteItem { get; set; }

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
