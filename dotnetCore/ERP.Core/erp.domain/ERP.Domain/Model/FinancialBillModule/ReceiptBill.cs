using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.StockBillModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class ReceiptBill : BaseEntity, IAggregationRoot, IDeleted, ICannotEditAndDelete
    {
        public ReceiptBill()
        {
            this.MoneyType = MoneyType.人民币;
            this.AuditState = AuditState.待审核;
            this.BusinessTime = DateTime.Now;
            this.OverdueTime = DateTime.Now;
        }

        #region 基本属性

        /// <summary>
        /// 应收金额
        /// </summary>
        [Display(Name = "应收金额")]
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 实收金额（应收金额 - 现金折扣 + 手续费）
        /// </summary>
        [Display(Name = "实收金额")]
        public decimal ReceiptAmount { get; set; }

        /// <summary>
        /// 现金金额
        /// </summary>
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 银行付款金额
        /// </summary>
        public decimal BankAmount { get; set; }

        /// <summary>
        /// 微信金额
        /// </summary>
        public decimal WeiXinAmount { get; set; }
        /// <summary>
        /// 支付宝金额
        /// </summary>
        public decimal AlipayAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public MoneyType MoneyType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public AuditState AuditState { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessTime { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime OverdueTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public bool IsInternal { get; set; }
        /// <summary>
        /// 关联的订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 关联的销售出单或退货入库单 编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        [MaxLength(64)]
        public string BillNo { get; set; }

        [MaxLength(64)]
        public string ReceivableBillNo { get; set; }
        public bool CannotEditAndDelete { get; set; }
        [MaxLength(64)]
        public string AgentSettlementNote_Id { get; set; }
        #endregion

        #region 聚合属性

        /// <summary>
        /// 收款单项
        /// </summary>
        public List<ReceiptBillItem> Items { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        [ForeignKey("CompanyUser")]
        [MaxLength(64)]
        public string CompanyUser_Id { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public User CompanyUser { get; set; }

        [ForeignKey("CompanyUserCity")]
        [MaxLength(64)]
        public string CompanyUserCity_Id { get; set; }
        public Org CompanyUserCity { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }
        [ForeignKey("ReceivableBill")]
        [MaxLength(64)]
        public string ReceivableBillId { get; set; }
        public ReceivableBill ReceivableBill { get; set; }

        #endregion

        #region 业务方法
        public void CheckAudit()
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");
        }

        public void CheckDelete()
        {
            if (this.CannotEditAndDelete)
                throw new BusinessException("当前单据不允许删除！");

            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");
        }

        public virtual void DeleteUpdateReceivableBillItems(ReceivableBill receivableBill)
        {
            List<ReceivableBillItem> items = receivableBill.Items;

            List<ReceiptBillItem> receiptBillItems = this.Items;
            ReceivableBillItem tempReceivableBillItem;
            foreach (var receiptBillItem in receiptBillItems)
            {
                tempReceivableBillItem = items.FirstOrDefault(i => i.Id == receiptBillItem.ReceivableBillItemId);
                if (tempReceivableBillItem != null)
                {
                    tempReceivableBillItem.HasReceiptAmount -= (receiptBillItem.ReceiptAmount + receiptBillItem.CounterFee);
                    if (receiptBillItem.ReceiptAmount > 0)
                    {
                        if (tempReceivableBillItem.HasReceiptAmount < 0) tempReceivableBillItem.HasReceiptAmount = decimal.Zero;
                    }
                    else
                    {
                        if (tempReceivableBillItem.HasReceiptAmount > 0) tempReceivableBillItem.HasReceiptAmount = decimal.Zero;
                    }
                }
            }
        }

        public virtual void AddUpdateReceivableBillItems(ReceivableBill receivableBill)
        {
            List<ReceivableBillItem> items = receivableBill.Items;
            List<ReceiptBillItem> receiptBillItems = this.Items;

            ReceivableBillItem tempReceivableBillItem;
            foreach (var receiptBillItem in receiptBillItems)
            {
                tempReceivableBillItem = items.FirstOrDefault(i => i.Id == receiptBillItem.ReceivableBillItemId);
                if (tempReceivableBillItem != null)
                {
                    tempReceivableBillItem.HasReceiptAmount += (receiptBillItem.ReceiptAmount + receiptBillItem.CounterFee);
                }
            }
        }

        public virtual void ComputeAmount()
        {
            this.CashAmount = this.Items.Where(i => i.Settlement == Settlement.货到付款现金).ToList().Sum(i => i.ReceiptAmount);
            this.BankAmount = this.Items.Where(i => i.Settlement == Settlement.货到付款银行卡).ToList().Sum(i => i.ReceiptAmount);
            this.WeiXinAmount = this.Items.Where(i => i.Settlement == Settlement.微信).ToList().Sum(i => i.ReceiptAmount);
            this.AlipayAmount = this.Items.Where(i => i.Settlement == Settlement.支付宝).ToList().Sum(i => i.ReceiptAmount);
        }

        #endregion

    }
}
