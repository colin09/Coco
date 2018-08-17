using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentSettlementNoteProductItem : BaseEntity
    {
        [ForeignKey("AgentSettlementNote")]
        [MaxLength(64)]
        public string AgentSettlementNote_Id { get; set; }
        public AgentSettlementNote AgentSettlementNote { get; set; }

        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        public Product Product { get; set; }

        /// <summary>
        /// 代理核算单：区域 销售明细结算单
        /// </summary>
        public List<AgentSettlementNoteItem> AgentSettlementNoteItems { get; set; }

        /// <summary>
        /// 代理核算单： 区域 应付明细
        /// </summary>
        public List<AgentSettlementPayableBillItem> AgentSettlementPayableBillItems { get; set; }

        /// <summary>
        /// 代理核算单 ： 总部代理城市 应收明细
        /// </summary>
        public List<AgentSettlementReceivableBillItem> AgentSettlementReceivableBillItems { get; set; }
    }
}
