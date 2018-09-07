using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 付款单
    /// </summary>
    public class PaymentBill : BaseEntity, IAggregationRoot, IDeleted, ICannotEditAndDelete
    {
        public PaymentBill()
        {
            this.AuditState = AuditState.待审核;
            this.Items = new List<PaymentBillItem>();
        }
        #region 基本属性
        /// <summary>
        /// 应付金额
        /// </summary>
        [Display(Name = "应付金额")]
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// 实付金额（应付金额 - 现金折扣 + 手续费）
        /// </summary>
        [Display(Name = "实付金额")]
        public decimal PaymentAmount { get; set; }

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
        [Display(Name = "币种")]
        public MoneyType MoneyType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public AuditState AuditState { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public bool IsInternal { get; set; }

        /// <summary>
        /// 关联的采购入库单或采购退货单编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        [MaxLength(64)]
        public string PayableBillNo { get; set; }
        [MaxLength(64)]
        public string BillNo { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [ForeignKey("PayableBill")]
        [MaxLength(64)]
        public string PayableBill_Id { get; set; }
        /// <summary>
        /// 应付单
        /// </summary>
        public PayableBill PayableBill { get; set; }
        [ForeignKey("Supplier")]
        [MaxLength(64)]
        public string Supplier_Id { get; set; }
        /// <summary>
        /// 收款供应商
        /// </summary>
        public Provider Supplier { get; set; }

        [ForeignKey("SupplierCity")]
        [MaxLength(64)]
        public string SupplierCity_Id { get; set; }
        public Org SupplierCity { get; set; }

        /// <summary>
        /// 付款单明细
        /// </summary>
        public List<PaymentBillItem> Items { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }

        public bool CannotEditAndDelete { get; set; }

        /// <summary>
        /// 代理结算单Id
        /// </summary>
        [MaxLength(64)]
        public string AgentSettlementNote_Id { get; set; }

        #endregion

        #region 业务方法
        public void CheckAudit()
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");
        }
        /*
        public virtual void Audit(AuditState auditState, ERPContext context, DateTime? auditTime = null)
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");

            //if (this.PaymentState == FinancialBillModule.PaymentState.未付款)
            //    throw new BusinessException("出纳未付款，不可审核！");

            this.AuditState = auditState;

            if (auditState == AuditState.已审核)
            {
                this.AuditTime = auditTime.HasValue ? auditTime.Value : DateTime.Now;

                List<SendoutDaily> list = new List<SendoutDaily>();
                SendoutDaily daily;

                foreach (var item in this.Items)
                {
                    var dailyType = item.Settlement.ToDailyType();
                    daily = list.FirstOrDefault(d => d.Year == AuditTime.Value.Year && d.Month == AuditTime.Value.Month && d.Day == AuditTime.Value.Day
                        && d.City.Id == this.City.Id
                         && ((d.Product == null && d.DailyType == dailyType)
                        || (!string.IsNullOrEmpty(item.ProductId) && d.Product != null && d.Product.Id == item.ProductId)));

                    if (daily == null)
                    {
                        daily = SendoutDaily.GetSendoutDaily(AuditTime.Value.Year, AuditTime.Value.Month, AuditTime.Value.Day,
                            item.ProductId, this.City, dailyType, context);
                        list.Add(daily);
                    }
                    daily.PayableAmount += item.PayableAmount;
                    daily.PaymentAmount += item.PaymentAmount;
                    daily.DiscountAmount += (item.DiscountPrice - item.CounterFee);
                }
            }
        }

        public virtual void UnAudit(ERPContext context)
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");
            //处理支出日报
            List<SendoutDaily> list = new List<SendoutDaily>();
            SendoutDaily daily;
            foreach (var item in this.Items)
            {
                var dailyType = item.Settlement.ToDailyType();
                daily = list.FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                    && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id
                    && ((!string.IsNullOrEmpty(item.ProductId) && d.Product != null && d.Product.Id == item.ProductId) || (d.Product == null && d.DailyType == dailyType)));
                if (daily == null)
                {
                    if (!string.IsNullOrWhiteSpace(item.ProductId))
                        daily = context.SendoutDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                   && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.Product.Id == item.ProductId);
                    else
                        daily = context.SendoutDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                   && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.DailyType == dailyType && d.Product == null);
                }
                if (daily != null)
                {
                    list.Add(daily);

                    daily.PayableAmount -= item.PayableAmount;
                    daily.PaymentAmount -= item.PaymentAmount;
                    daily.DiscountAmount -= (item.DiscountPrice - item.CounterFee);
                }
            }

            foreach (var item in list)
            {
                if (item.PaymentAmount == Decimal.Zero && item.PayableAmount == Decimal.Zero && item.DiscountAmount == Decimal.Zero)
                    context.SendoutDailys.Remove(item);
            }

            this.AuditState = AuditState.待审核;
            this.AuditTime = null;
        }

        public virtual void Delete(ERPContext context)
        {
            if (this.IsDeleted)
                return;
            if (this.CannotEditAndDelete)
            {
                throw new BusinessException("当前单据不允许删除！");
            }
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");
            this.IsDeleted = true;

            var payableBill = context.PayableBills.Include("City").Include("AllocationNote.ToCity")
                .Include("AllocationNote.FromCity").WhereNotDeleted()
                .FirstOrDefault(b => b.Id == this.PayableBill.Id);

            if (payableBill != null)
            {
                payableBill.IsDownReason = false;
                payableBill.IsPayment = false;
                payableBill.HasPaymentAmount -= this.PaymentAmount;

                payableBill.UpdateAllocationSettlementNoteSettlementAmount(context, 0 - this.PaymentAmount);

                DeleteUpdatePayableBillItems(payableBill);
            }
        }
        public void CheckDelete()
        {
            if (this.IsDeleted)
                return;
            if (this.CannotEditAndDelete)
            {
                throw new BusinessException("当前单据不允许删除！");
            }
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");
        }
        public virtual void DeleteUpdatePayableBillItems(PayableBill payableBill)
        {
            List<PayableBillItem> items = payableBill.Items;
            List<PaymentBillItem> paymentBillItems = this.Items;

            PayableBillItem tempPayableBillItem;
            foreach (var paymentBillItem in paymentBillItems)
            {
                tempPayableBillItem = items.FirstOrDefault(i => i.Id == paymentBillItem.PayableBillItemId);
                if (tempPayableBillItem != null)
                {
                    tempPayableBillItem.HasPaymentAmount -= (paymentBillItem.PaymentAmount - paymentBillItem.CounterFee);
                    if (paymentBillItem.PaymentAmount > 0)
                    {
                        if (tempPayableBillItem.HasPaymentAmount < 0) tempPayableBillItem.HasPaymentAmount = decimal.Zero;
                    }
                    else
                    {
                        if (tempPayableBillItem.HasPaymentAmount > 0) tempPayableBillItem.HasPaymentAmount = decimal.Zero;
                    }
                }
            }
        }

        public virtual void AddUpdatePayableBillItems(PayableBill payableBill)
        {
            List<PayableBillItem> items = payableBill.Items;

            List<PaymentBillItem> paymentBillItems = this.Items;
            PayableBillItem tempPayableBillItem;
            foreach (var paymentBillItem in paymentBillItems)
            {
                tempPayableBillItem = items.FirstOrDefault(i => i.Id == paymentBillItem.PayableBillItemId);
                if (tempPayableBillItem != null)
                {
                    tempPayableBillItem.HasPaymentAmount += (paymentBillItem.PaymentAmount - paymentBillItem.CounterFee);
                }
            }
        }
        public virtual void ComputeAmount()
        {
            this.CashAmount = this.Items.Where(i => i.Settlement == Settlement.货到付款现金).ToList().Sum(i => i.PaymentAmount);
            this.BankAmount = this.Items.Where(i => i.Settlement == Settlement.货到付款银行卡).ToList().Sum(i => i.PaymentAmount);
            this.WeiXinAmount = this.Items.Where(i => i.Settlement == Settlement.微信).ToList().Sum(i => i.PaymentAmount);
            this.AlipayAmount = this.Items.Where(i => i.Settlement == Settlement.支付宝).ToList().Sum(i => i.PaymentAmount);
        }
        */
        #endregion


    }
}
