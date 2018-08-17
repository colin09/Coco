using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 应付单明细
    /// </summary>
    public class PayableBillItem : BaseEntity
    {
        #region 基本属性

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
        /// 含税单价
        /// </summary>
        public decimal IncludeTaxPrice { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 不含税金额 todo：计算公式
        /// </summary>
        public decimal NotTaxAmount { get; set; }

        /// <summary>
        /// 税额
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        /// <summary>
        /// 已付款金额
        /// </summary>
        public decimal HasPaymentAmount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        #endregion

        /// <summary>
        /// 关联的单据项Id
        /// </summary>
        [MaxLength(64)]
        public string NoteItemId { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PayableBill")]
        public string PayableBill_Id { get; set; }
        /// <summary>
        /// 应付单
        /// </summary>
        public PayableBill PayableBill { get; set; }

        /// <summary>
        /// 产品（物料）Id
        /// </summary>
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        /// <summary>
        /// 产品（物料）
        /// </summary>
        [MaxLength(100)]
        public string ProductName { get; set; }

        #endregion
    }
}
