using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.FinancialDailyModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialDailyModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class PrepayBill : BaseEntity, IAggregationRoot, IDeleted
    {
        public PrepayBill()
        {
            this.WriteOffAmount = 0;
            this.PrepayBillType = PrepayBillType.预付单;
            this.AuditState = AuditState.待审核;
            this.WriteOffState = WriteOffState.未核销;
            this.AuditTime = null;
        }

        #region 基本信息

        /// <summary>
        /// 预付类型
        /// </summary>
        public PrepayBillType PrepayBillType { get; set; }

        /// <summary>
        /// 预付单编号
        /// </summary>
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 采购申请单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseRequisitionNo { get; set; }

        /// <summary>
        /// 预付金额
        /// </summary>
        [Display(Name = "预付金额")]
        public decimal PrepayAmount { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 预付方式
        /// </summary>
        [Display(Name = "预付方式")]
        public Settlement Settlement { get; set; }

        /// <summary>
        /// 是否集团内部往来
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }
        /// <summary>
        /// 采购人员Id
        /// </summary>
        [MaxLength(64)]
        public string PurchaseUserId { get; set; }
        /// <summary>
        /// 采购人员
        /// </summary>
        [MaxLength(64)]
        public string PurchaseName { get; set; }
        /// <summary>
        /// 采购联系方式 
        /// </summary>
        [MaxLength(64)]
        public string PurchaseMobileNo { get; set; }


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
        [ForeignKey("Provider")]
        public string Provider_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }
        [MaxLength(64)]
        [ForeignKey("SupplierCity")]
        public string SupplierCity_Id { get; set; }
        public Org SupplierCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("PurchaseRequisition")]
        public string PurchaseRequisition_Id { get; set; }
        /// <summary>
        /// 所属采购申请单
        /// </summary>
        public PurchaseRequisition PurchaseRequisition { get; set; }

        #endregion

        #region 业务方法

        public virtual decimal GetCanWriteOffAmount()
        {
            return this.PrepayAmount - this.WriteOffAmount - this.RefundAmount;
        }

        public virtual void ChangeWriteOffState()
        {
            if (this.WriteOffAmount == Decimal.Zero && this.RefundAmount == Decimal.Zero)
                this.WriteOffState = WriteOffState.未核销;
            else if (this.WriteOffAmount + this.RefundAmount < this.PrepayAmount)
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

                if (this.PrepayBillType == PrepayBillType.预付单)
                {
                    SendoutDaily daily = SendoutDaily.GetSendoutDaily(AuditTime.Value.Year, AuditTime.Value.Month, AuditTime.Value.Day,
                               null, this.City, DailyType.预付款, context);

                    daily.PayableAmount += this.PrepayAmount;
                    daily.PaymentAmount += this.PrepayAmount;
                }
            }
        }

        public void UnAudit(ERPContext context)
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            //判断是否有核销单据
            if (context.WriteOffPrepayBillItems.Any(i => i.PrepayBill.Id == this.Id && !i.PrepayWriteOffBill.IsDeleted))
                throw new BusinessException("已存在预付应付核销单据，不能反审核");

            if (context.PrepayRefundBills.WhereNotDeleted().Any(b => b.PrepayBill.Id == this.Id))
                throw new BusinessException("存在预付退款单，不能反审核！");

            if (this.PrepayBillType == PrepayBillType.预付单)
            {
                //处理支出日报
                SendoutDaily daily = context.SendoutDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == this.AuditTime.Value.Year && d.Month == this.AuditTime.Value.Month
                       && d.Day == this.AuditTime.Value.Day && d.City.Id == this.City.Id && d.DailyType == DailyType.预付款 && d.Product == null);

                if (daily != null)
                {
                    daily.PayableAmount -= this.PrepayAmount;
                    daily.PaymentAmount -= this.PrepayAmount;
                    if (daily.PaymentAmount == Decimal.Zero && daily.PayableAmount == Decimal.Zero && daily.DiscountAmount == Decimal.Zero)
                        context.SendoutDailys.Remove(daily);
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
        }*/

        #endregion
    }
}
