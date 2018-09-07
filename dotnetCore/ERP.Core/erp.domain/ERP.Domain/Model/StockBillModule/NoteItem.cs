using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.SettlementNoteItemModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    public class NoteItem : BaseEntity
    {
        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 补充利润（只用来计算折前净利）
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 兑奖红包金额
        /// </summary>
        public decimal UseRewardBonusAmount { get; set; }

        /// <summary>
        /// 成本价格
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 是否是采购赠品
        /// </summary>
        public bool IsGiveaway { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 销售模式
        /// </summary>
        public OrderItemSaleMode SaleMode { get; set; }

        /// <summary>
        /// ERP销售模式
        /// </summary>
        public ERPSaleMode ERPSaleMode { get; set; }

        #region 聚合属性

        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        [ForeignKey("SaleProduct")]
        [MaxLength(64)]
        public string SaleProduct_Id { get; set; }
        /// <summary>
        /// 销售产品
        /// </summary>
        public Product SaleProduct { get; set; }

        [ForeignKey("OrderOutStockNote")]
        [MaxLength(64)]
        public string OrderOutStockNote_Id { get; set; }

        public OrderOutStockNote OrderOutStockNote { get; set; }

        [ForeignKey("AllocationNoteItem")]
        [MaxLength(64)]
        public string AllocationNoteItem_Id { get; set; }
        /// <summary>
        /// 关联的调拨项
        /// </summary>
        public AllocationNoteItem AllocationNoteItem { get; set; }

        [ForeignKey("SettlementNoteItem")]
        [MaxLength(64)]
        public string SettlementNoteItem_Id { get; set; }
        /// <summary>
        /// 关联的结算单项
        /// </summary>
        public SettlementNoteItem SettlementNoteItem { get; set; }

        #endregion
    }
}
