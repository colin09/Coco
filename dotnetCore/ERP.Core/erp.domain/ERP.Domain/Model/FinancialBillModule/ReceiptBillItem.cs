using ERP.Domain.Enums.FinancialBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.FinancialBillModule
{
    public class ReceiptBillItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 结算方式
        /// </summary>
        [Display(Name = "结算方式")]
        public Settlement Settlement { get; set; }

        /// <summary>
        /// 收款用途
        /// </summary>
        [Display(Name = "收款用途")]
        public ReceiptPurpose ReceiptPurpose { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        [Display(Name = "应收金额")]
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 实收金额（应收金额 - 现金折扣 - 手续费）
        /// </summary>
        [Display(Name = "实收金额")]
        public decimal ReceiptAmount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        [Display(Name = "折扣金额")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal CounterFee { get; set; }

        /// <summary>
        /// 我方银行卡号
        /// </summary>
        [Display(Name = "我方银行卡号")]
        [MaxLength(64)]
        public string OurBankNo { get; set; }

        /// <summary>
        /// 结算号
        /// </summary>
        [Display(Name = "结算号")]
        [MaxLength(100)]
        public string SettlementNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(256)]
        public string Remark { get; set; }

        #endregion

        #region 聚合关系
        /// <summary>
        /// 产品Id
        /// </summary>
        [MaxLength(64)]
        public string ProductId { get; set; }

        /// <summary>
        /// 关联的应收单项Id
        /// </summary>
        [MaxLength(64)]
        public string ReceivableBillItemId { get; set; }

        [ForeignKey("ReceiptBill")]
        [MaxLength(64)]
        public string ReceiptBill_Id { get; set; }

        public ReceiptBill ReceiptBill { get; set; }

        #endregion
    }
}
