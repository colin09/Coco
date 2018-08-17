using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.CostAdjustModule
{
    /// <summary>
    /// 成本调整单
    /// </summary>
    public class CostAdjustNote : BaseEntity, IAggregationRoot, IDeleted
    {
        public CostAdjustNote()
        {
            this.AuditState = NoteAuditState.待审核;
            this.BusinessTime = DateTime.Now;
        }

        public decimal AdjustAmount { get; set; }
        public NoteAuditState AuditState { get; set; }
        [MaxLength(200)]
        public string AuditRemark { get; set; }

        public DateTime BusinessTime { get; set; }

        public bool IsDeleted { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        public City City { get; set; }

        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }

        #endregion

        #region 方法
        /* 
        public virtual void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == NoteAuditState.审核通过)
            {
                //核算
                DateTime now = DateTime.Now;
                FinancialStock stock = GetFinancialStock(now);

                FinancialStock endingBalance = FinancialStock.GetEndingBalance(this.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);

                stock.Accounting(context, endingBalance);

                context.FinancialStocks.Add(stock);

                this.AuditState = NoteAuditState.已核算;
            }
        }

        private FinancialStock GetFinancialStock(DateTime accountingTime)
        {
            FinancialStock financialStock = new FinancialStock()
            {
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.Id,
                BusinessItemId = this.Id,
                BusinessTime = accountingTime,
                IncomeAmount = this.AdjustAmount,
                StoreHouse = this.StoreHouse,
                City = this.StoreHouse.City,
                Product = this.Product,
                Type = FinancialStockType.成本调整单
            };
            return financialStock;
        }
        private FinancialStock GetFinancialStock(DateTime accountingTime, FinancialStock lastFinancialStock)
        {
            FinancialStock financialStock = new FinancialStock()
            {
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.Id,
                BusinessItemId = this.Id,
                BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
                IncomeAmount = this.AdjustAmount,
                StoreHouse = this.StoreHouse,
                City = this.StoreHouse.City,
                Product = this.Product,
                Type = FinancialStockType.成本调整单
            };

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            return financialStock;
        }

        public virtual void CrossMonthAuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == NoteAuditState.审核通过)
            {
                //核算
                HandleCrossMonthAccounting(context);

                this.AuditState = NoteAuditState.已核算;
            }
        }

        private void HandleCrossMonthAccounting(ERPContext context)
        {
            DateTime accountingTime = this.BusinessTime;//结算时间

            var lastFinancialStock = FinancialStock.GetCrossMonthLastBalance(this.Product, accountingTime.Year, accountingTime.Month
              , this.StoreHouse, this.StoreHouse.City, context);

            var followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime, this.Product, this.StoreHouse, this.StoreHouse.City, context);

            var financialStock = GetFinancialStock(accountingTime, lastFinancialStock);

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks);
            context.FinancialStocks.Add(financialStock);
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="context"></param>
        public virtual void UnAudit(ERPContext context)
        {
            if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (this.AuditState == NoteAuditState.已核算)
            {
                var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
                    .FirstOrDefault(s => s.BusinessItemId == this.Id && s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);

                financialStock.UnAudit(context);
            }

            this.AuditState = NoteAuditState.待审核;
            this.AuditRemark = "";
        }


        public virtual void Delete()
        {
            if (this.IsDeleted) return;
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            this.IsDeleted = true;
        }
        */
        #endregion
    }
}
