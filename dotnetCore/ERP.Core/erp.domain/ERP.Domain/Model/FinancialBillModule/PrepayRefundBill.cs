using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialDailyModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class PrepayRefundBill : BaseEntity, IAggregationRoot, IDeleted
    {
        public PrepayRefundBill()
        {
            this.AuditState = AuditState.待审核;
        }

        #region 基本信息
        [MaxLength(64)]
        public string BillNo { get; set; }
        [MaxLength(64)]
        public string PrepayBillNo { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        public AuditState AuditState { get; set; }
        public DateTime? AuditTime { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region 聚合信息

        [MaxLength(64)]
        [ForeignKey("PrepayBill")]
        public string PrepayBill_Id { get; set; }
        /// <summary>
        /// 预付单
        /// </summary>
        public PrepayBill PrepayBill { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
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

                SendoutDaily daily = SendoutDaily.GetSendoutDaily(AuditTime.Value.Year, AuditTime.Value.Month, AuditTime.Value.Day,
                           null, this.City, DailyType.预付退款, context);

                daily.PayableAmount -= this.RefundAmount;
                daily.PaymentAmount -= this.RefundAmount;

            }
        }

        public void UnAudit(ERPContext context)
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            //处理支出日报
            SendoutDaily daily = context.SendoutDailys.Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                   && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.DailyType == DailyType.预付退款 && d.Product == null);

            if (daily != null)
            {
                daily.PayableAmount += this.RefundAmount;
                daily.PaymentAmount += this.RefundAmount;
                if (daily.PaymentAmount == Decimal.Zero && daily.PayableAmount == Decimal.Zero && daily.DiscountAmount == Decimal.Zero)
                    context.SendoutDailys.Remove(daily);
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

            this.PrepayBill.RefundAmount -= this.RefundAmount;
            this.PrepayBill.ChangeWriteOffState();

            this.IsDeleted = true;
        }*/
        
        #endregion

    }
}
