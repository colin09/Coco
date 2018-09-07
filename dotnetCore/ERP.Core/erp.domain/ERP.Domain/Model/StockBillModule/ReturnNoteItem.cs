using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    public class ReturnNoteItem : BaseEntity
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
        /// 价税合计
        /// </summary>
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 成本价格
        /// 退货入库单的成本价  是销售出库单成本核算时的成本价
        /// 这个成本价 应是 退货订单 下推生成 退货入库单时保存下来的。
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }
        public bool IsGiveaway { get; set; }

        /// <summary>
        /// 销售模式
        /// </summary>
        public OrderItemSaleMode SaleMode { get; set; }

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

        [ForeignKey("ReturnOrderInStockNote")]
        [MaxLength(64)]
        public string ReturnOrderInStockNote_Id { get; set; }
        public ReturnOrderInStockNote ReturnOrderInStockNote { get; set; }

        #endregion
    }
}
