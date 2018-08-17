using ERP.Domain.Context;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrgModule;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialDailyModule;

namespace ERP.Domain.Model.FinancialBillModule
{
    public class PreReceiptRefundBill : BaseEntity, IAggregationRoot, IDeleted
    {
        public PreReceiptRefundBill()
        {
            this.AuditState = AuditState.待审核;
        }

        #region 基本属性

        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        public AuditState AuditState { get; set; }
        public DateTime? AuditTime { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// 预收单编号
        /// </summary>
        [MaxLength(64)]
        public string PreReceiptBillNo { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PreReceiptBill")]
        public string PreReceiptBill_Id { get; set; }
        public PreReceiptBill PreReceiptBill { get; set; }

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法
        /* 
        public virtual void Audit(AuditState auditState, ERPContext context, DateTime? auditTime = null)
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;

            if (auditState == AuditState.已审核)
            {
                this.AuditTime = auditTime.HasValue ? auditTime.Value : DateTime.Now;

                IncomeDaily daily = IncomeDaily.GetIncomeDaily(AuditTime.Value.Year, AuditTime.Value.Month, AuditTime.Value.Day
                            , null, this.City, DailyType.预收退款, context);

                daily.ReceiptAmount -= this.RefundAmount;
                daily.ReceivableAmount -= this.RefundAmount;
                daily.GrossProfit -= this.RefundAmount;
                daily.NetProfit -= this.RefundAmount;
            }
        }

        public void UnAudit(ERPContext context)
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            //处理收入日报
            IncomeDaily daily = context.IncomeDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                    && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.DailyType == DailyType.预收款 && d.Product == null);

            if (daily != null)
            {
                daily.ReceiptAmount += this.RefundAmount;
                daily.ReceivableAmount += this.RefundAmount;
                daily.GrossProfit += this.RefundAmount;
                daily.NetProfit += this.RefundAmount;

                if (daily.ReceiptAmount == Decimal.Zero && daily.ReceivableAmount == Decimal.Zero)
                    context.IncomeDailys.Remove(daily);
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

            this.PreReceiptBill.RefundAmount -= this.RefundAmount;
            this.PreReceiptBill.ChangeWriteOffState();

            this.IsDeleted = true;
        }
        */
        #endregion
    }
}
