using ERP.Domain.Enums.FinancialBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 付款单明细
    /// </summary>
    public class PaymentBillItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 结算方式
        /// </summary>
        public Settlement Settlement { get; set; }

        /// <summary>
        /// 付款用途
        /// </summary>
        public PaymentPurpose PaymentPurpose { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountPrice { get; set; }

        /// <summary>
        /// 折后金额（应付金额 - 现金折扣）
        /// </summary>
        public decimal DiscountAfterPrice { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal CounterFee { get; set; }

        /// <summary>
        /// 实付金额（应付金额 - 现金折扣 + 手续费）
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// 我方银行卡号
        /// </summary>
        [MaxLength(64)]
        public string OurBankNo { get; set; }

        /// <summary>
        /// 结算号
        /// </summary>
        [MaxLength(100)]
        public string SettlementNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        [MaxLength(64)]
        public string ProductId { get; set; }

        /// <summary>
        /// 关联的应付单项Id
        /// </summary>
        [MaxLength(64)]
        public string PayableBillItemId { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PaymentBill")]
        public string PaymentBill_Id { get; set; }
        /// <summary>
        /// 付款单
        /// </summary>
        public PaymentBill PaymentBill { get; set; }

        #endregion
    }
}
