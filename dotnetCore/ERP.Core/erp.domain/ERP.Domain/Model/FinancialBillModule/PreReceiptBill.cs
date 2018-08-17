using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.FinancialDailyModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class PreReceiptBill : BaseEntity, IAggregationRoot, IDeleted
    {
        public PreReceiptBill()
        {
            this.WriteOffAmount = 0;
            this.PreReceiptBillType = PreReceiptBillType.预收单;
            this.AuditState = AuditState.待审核;
            this.WriteOffState = WriteOffState.未核销;
        }

        #region 基本信息

        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 预收单类型
        /// </summary>
        public PreReceiptBillType PreReceiptBillType { get; set; }

        /// <summary>
        /// 预收金额
        /// </summary>
        [Display(Name = "预收金额")]
        public decimal PreReceiptAmount { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 预收方式
        /// </summary>
        [Display(Name = "预收方式")]
        public Settlement Settlement { get; set; }

        /// <summary>
        /// 是否集团内部往来
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 核销状态
        /// </summary>
        public WriteOffState WriteOffState { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditState AuditState { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region 聚合信息

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        [MaxLength(64)]
        [ForeignKey("CompanyUser")]
        public string CompanyUser_Id { get; set; }
        public User CompanyUser { get; set; }

        [MaxLength(64)]
        [ForeignKey("CompanyUserCity")]
        public string CompanyUserCity_Id { get; set; }
        public Org CompanyUserCity { get; set; }

        #endregion

        #region 业务方法

        public virtual decimal GetCanWriteOffAmount()
        {
            return this.PreReceiptAmount - this.WriteOffAmount - this.RefundAmount;
        }

        public virtual void ChangeWriteOffState()
        {
            if (this.WriteOffAmount == Decimal.Zero && this.RefundAmount == Decimal.Zero)
                this.WriteOffState = WriteOffState.未核销;
            else if (this.WriteOffAmount + this.RefundAmount < this.PreReceiptAmount)
                this.WriteOffState = WriteOffState.部分核销;
            else
                this.WriteOffState = WriteOffState.已核销;
        }
        /*
        public virtual void Audit(AuditState auditState, ERPContext context, DateTime? auditTime = null)
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;

            if (auditState == AuditState.已审核)
            {
                this.AuditTime = auditTime.HasValue ? auditTime.Value : DateTime.Now;

                if (this.PreReceiptBillType == PreReceiptBillType.预收单)
                {
                    IncomeDaily daily = IncomeDaily.GetIncomeDaily(AuditTime.Value.Year, AuditTime.Value.Month, AuditTime.Value.Day
                                , null, this.City, DailyType.预收款, context);

                    daily.ReceiptAmount += this.PreReceiptAmount;
                    daily.ReceivableAmount += this.PreReceiptAmount;

                    daily.GrossProfit += this.PreReceiptAmount;
                    daily.NetProfit += this.PreReceiptAmount;
                }
            }
        }

        public void UnAudit(ERPContext context)
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            //判断是否有核销单据
            if (context.WriteOffPreReceiptBillItems.Any(i => i.PreReceiptBill.Id == this.Id && !i.PreReceiptWriteOffBill.IsDeleted))
                throw new BusinessException("已存在预收应收核销单据，不能反审核！");

            if (context.PreReceiptRefundBills.WhereNotDeleted().Any(b => b.PreReceiptBill.Id == this.Id))
                throw new BusinessException("存在预收退款单，不能反审核！");

            if (this.PreReceiptBillType == PreReceiptBillType.预收单)
            {
                //处理收入日报
                IncomeDaily daily = context.IncomeDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                        && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.DailyType == DailyType.预收款 && d.Product == null);

                if (daily != null)
                {
                    daily.ReceiptAmount -= this.PreReceiptAmount;
                    daily.ReceivableAmount -= this.PreReceiptAmount;
                    daily.GrossProfit -= this.PreReceiptAmount;
                    daily.NetProfit -= this.PreReceiptAmount;

                    if (daily.ReceiptAmount == Decimal.Zero && daily.ReceivableAmount == Decimal.Zero)
                        context.IncomeDailys.Remove(daily);
                }
            }

            this.AuditState = AuditState.待审核;
            this.AuditTime = null;
        }

        public virtual void Delete(ERPContext context)
        {
            if (this.IsDeleted)
                return;
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");

            this.IsDeleted = true;
        }
        */
        #endregion

    }
}
