using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.SettlementNoteItemModule
{
    /// <summary>
    /// 销售出库单明细结算单
    /// </summary>
    public class SettlementNoteItem : BaseEntity, IAggregationRoot, IHidden
    {
        /// <summary>
        /// 销售出库单编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNO { get; set; }

        /// <summary>
        /// 销售类型
        /// </summary>
        public SaleBusinessType SaleBusinessType { get; set; }

        /// <summary>
        /// 出库日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 已结算
        /// </summary>
        public bool HasSettlement { get; set; }

        /// <summary>
        /// 关联结算单号
        /// </summary>
        [MaxLength(64)]
        public string SettlementNo { get; set; }
        public bool IsHidden { get; set; }

        public City City { get; set; }
        public StoreHouse StoreHouse { get; set; }
        public Product Product { get; set; }
    }
}
