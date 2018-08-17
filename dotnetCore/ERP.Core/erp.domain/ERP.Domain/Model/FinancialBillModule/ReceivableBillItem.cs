using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.FinancialBillModule
{
    public class ReceivableBillItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Num { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Display(Name = "单价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        [Display(Name = "含税单价")]
        public decimal IncludeTaxPrice { get; set; }

        /// <summary>
        /// 税额
        /// </summary>
        [Display(Name = "税额")]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// 折扣额
        /// </summary>
        [Display(Name = "折扣额")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        [Display(Name = "价税合计")]
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        /// <summary>
        /// 已收款金额
        /// </summary>
        public decimal HasReceiptAmount { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal TotalAmount { get { return this.AdValoremAmount + this.DiscountAmount; } }

        /// <summary>
        /// 关联的单据项Id
        /// </summary>
        [MaxLength(64)]
        public string NoteItemId { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("ReceivableBill")]
        [MaxLength(64)]
        public string ReceivableBill_Id { get; set; }
        /// <summary>
        /// 所属应收单
        /// </summary>
        public ReceivableBill ReceivableBill { get; set; }
        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }

        /// <summary>
        /// 冗余产品名字
        /// </summary>
        [MaxLength(100)]
        public string ProductName { get; set; }

        /// <summary>
        /// 物料
        /// </summary>
        public Product Product { get; set; }

        #endregion

        #region 方法

        public void ComputeAdValoremAmount()
        {
            this.AdValoremAmount = this.Num * this.IncludeTaxPrice - this.DiscountAmount;
        }

        #endregion
    }
}
