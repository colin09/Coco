using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.ProductModule
{
    /// <summary>
    /// 物料调拨
    /// </summary>
    public class ProductAllocation : BaseEntity, IAggregationRoot, IDeleted
    {
        public ProductAllocation()
        {
            this.AuditState = NoteAuditState.待审核;
            this.Items = new List<ProductAllocationItem>();
        }
        #region 基本属性

        /// <summary>
        /// 订单号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        [Display(Name = "日期")]
        public DateTime BusinessTime { get; set; }

        public NoteAuditState AuditState { get; set; }

        [MaxLength(255)]
        public string AuditRemark { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region 聚合属性

        public List<ProductAllocationItem> Items { get; set; }
        [MaxLength(64)]
        [ForeignKey("From")]
        public string From_Id { get; set; }
        public StoreHouse From { get; set; }
        [MaxLength(64)]
        [ForeignKey("To")]
        public string To_Id { get; set; }
        public StoreHouse To { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法
        /*
        public void UnAudit(ERPContext context)
        {
            if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");
            ProductStock fromStock;
            ProductStock toStock;
            FinancialStock fromFinianlStock;
            FinancialStock toFinianlStock;
            ProductLotManager lotManager = new ProductLotManager(context);
            foreach (var item in this.Items)
            {
                fromStock = context.ProductStocks.FirstOrDefault(s => s.StoreHouse.Id == this.From.Id && s.Product.Id == item.Product.Id);
                if (fromStock == null)
                {
                    fromStock = new ProductStock()
                    {
                        City_Id = this.City.Id,
                        City = this.City,
                        Product = item.Product,
                        StoreHouse = this.To,
                        CostPrice = item.Price,
                        PriceUnit = item.Unit,
                        StockUnit = item.Unit,
                    };
                    context.ProductStocks.Add(fromStock);
                }
                fromStock.AddStock(item.Count, item.Price);

                toStock = context.ProductStocks.FirstOrDefault(s => s.StoreHouse.Id == this.To.Id && s.Product.Id == item.Product.Id);
                if (toStock == null || toStock.StockNum < item.Count)
                    throw new BusinessException("拨入仓库库存不足！");

                toStock.ReduceStock(item.Count, item.Price);

                if (this.AuditState == NoteAuditState.已核算)
                {
                    fromFinianlStock = context.FinancialStocks.Include("StoreHouse").Include("Product").FirstOrDefault(s =>
                         s.BusinessItemId == this.Id && s.City.Id == this.City.Id
                        && s.StoreHouse.Id == this.From.Id && s.Product.Id == item.Product.Id);
                    fromFinianlStock.UnAudit(context);

                    toFinianlStock = context.FinancialStocks.Include("StoreHouse").Include("Product").FirstOrDefault(s =>
                         s.BusinessItemId == this.Id && s.City.Id == this.City.Id
                        && s.StoreHouse.Id == this.To.Id && s.Product.Id == item.Product.Id);
                    toFinianlStock.UnAudit(context);
                }
                lotManager.AddProductLot(this.From, item.Product, item.Count);
                lotManager.ReduceProductLot(this.To, item.Product, item.Count);
            }

            this.AuditState = NoteAuditState.待审核;
            this.AuditRemark = "";
        }

        public void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == NoteAuditState.审核通过)
            {
                ProductLotManager lotManager = new ProductLotManager(context);
                ProductStock fromProductStock;
                ProductStock toProductStock;

                DateTime now = DateTime.Now;
                FinancialStock fromFinancialStock;
                FinancialStock toFinancialStock;
                FinancialStock fromEndingBalance;
                FinancialStock toEndingBalance;

                foreach (var item in this.Items)
                {
                    HandleProductStock(context, item, out fromProductStock, out toProductStock);

                    fromEndingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month, this.From, this.City, context);
                    fromFinancialStock = new FinancialStock()
                    {
                        Type = FinancialStockType.物料调拨单,
                        City = this.City,
                        Year = now.Year,
                        Month = now.Month,
                        BusinessId = this.NoteNo,
                        BusinessItemId = this.Id,
                        BusinessTime = now,
                        StoreHouse = this.From,
                        Product = item.Product,
                        SendoutNum = item.Count,
                        SendoutPrice = item.Price,
                        SendoutAmount = item.Count * item.Price,
                    };
                    fromFinancialStock.Accounting(context, fromEndingBalance, fromProductStock);
                    context.FinancialStocks.Add(fromFinancialStock);

                    toEndingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month, this.To, this.City, context);
                    toFinancialStock = new FinancialStock()
                    {
                        Type = FinancialStockType.物料调拨单,
                        City = this.City,
                        Year = now.Year,
                        Month = now.Month,
                        BusinessId = this.NoteNo,
                        BusinessItemId = this.Id,
                        BusinessTime = now,
                        StoreHouse = this.To,
                        Product = item.Product,
                        IncomeNum = item.Count,
                        IncomePrice = item.Price,
                        IncomeAmount = item.Count * item.Price,
                    };
                    toFinancialStock.Accounting(context, toEndingBalance, toProductStock);
                    context.FinancialStocks.Add(toFinancialStock);
                    lotManager.ReduceProductLot(this.From, item.Product, item.Count);
                    lotManager.AddProductLot(this.To, item.Product, item.Count);
                }
                this.AuditState = NoteAuditState.已核算;
            }
        }

        public void CrossMonthAuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == NoteAuditState.审核通过)
            {
                ProductLotManager lotManager = new ProductLotManager(context);
                //缓存所有 Item设计的 产品库存
                List<ProductStock> productStockList = new List<ProductStock>();

                //缓存所有 Item的最后一条存货收发存明细 用于计算结存并避免出现重复物料造成的 多次计算问题
                List<FinancialStock> lastFinancialStockList = new List<FinancialStock>();

                //缓存 所有Item 结算 所影响的 存货收发存明细
                Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic = new Dictionary<string, List<FinancialStock>>(this.Items.Count);

                ProductStock fromProductStock;
                ProductStock toProductStock;//临时变量——产品库存

                foreach (var item in this.Items)
                {
                    //处理库存
                    HandleProductStock(context, item, out fromProductStock, out toProductStock);

                    // 核算
                    HandleCrossMonthAccounting(item, followUpFinancialStocksDic, lastFinancialStockList, fromProductStock, toProductStock, context);
                    lotManager.ReduceProductLot(this.From, item.Product, item.Count);
                    lotManager.AddProductLot(this.To, item.Product, item.Count);

                }
                this.AuditState = NoteAuditState.已核算;
            }
        }

        private void HandleProductStock(ERPContext context, ProductAllocationItem item, out ProductStock fromProductStock, out ProductStock toProductStock)
        {
            fromProductStock = context.ProductStocks.FirstOrDefault(s => s.StoreHouse.Id == this.From.Id && s.Product.Id == item.Product.Id);
            if (fromProductStock == null || fromProductStock.StockNum < item.Count)
                throw new BusinessException("拨出仓库库存不足！");

            fromProductStock.ReduceStock(item.Count, item.Price);

            toProductStock = context.ProductStocks.FirstOrDefault(s => s.StoreHouse.Id == this.To.Id && s.Product.Id == item.Product.Id);
            if (toProductStock == null)
            {
                toProductStock = new ProductStock()
                {
                    City_Id = this.City.Id,
                    City = this.City,
                    Product = item.Product,
                    StoreHouse = this.To,
                    CostPrice = item.Price,
                    PriceUnit = item.Unit,
                    StockUnit = item.Unit,
                };
                context.ProductStocks.Add(toProductStock);
            }
            toProductStock.AddStock(item.Count, item.Price);
        }
        private void HandleCrossMonthAccounting(ProductAllocationItem item, Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic, List<FinancialStock> lastFinancialStockList, ProductStock fromProductStock, ProductStock toProductStock, ERPContext context)
        {
            DateTime accountingTime = this.BusinessTime;

            var fromLastFinancialStock = lastFinancialStockList.FirstOrDefault(l => l.Product.Id == item.Product.Id && l.StoreHouse.Id == this.From.Id);
            if (fromLastFinancialStock == null)
            {
                fromLastFinancialStock = FinancialStock.GetCrossMonthLastBalance(item.Product, accountingTime.Year, accountingTime.Month
                , this.From, this.City, context);
            }
            else
            {
                lastFinancialStockList.Remove(fromLastFinancialStock);
            }

            string key = string.Format("{0}_{1}_{2}", this.City.Id, item.Product.Id, this.From.Id);
            List<FinancialStock> fromFollowUpFinancialStocks;
            followUpFinancialStocksDic.TryGetValue(key, out fromFollowUpFinancialStocks);
            if (fromFollowUpFinancialStocks == null || fromFollowUpFinancialStocks.Count == 0)
            {
                fromFollowUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(fromLastFinancialStock.BusinessTime, item.Product, this.From, this.City, context);
                followUpFinancialStocksDic.Add(key, fromFollowUpFinancialStocks);
            }

            var fromFinancialStock = new FinancialStock()
             {
                 Type = FinancialStockType.物料调拨单,
                 City = this.City,
                 Year = accountingTime.Year,
                 Month = accountingTime.Month,
                 BusinessId = this.NoteNo,
                 BusinessItemId = this.Id,
                 BusinessTime = fromLastFinancialStock.BusinessTime.AddMilliseconds(2),
                 StoreHouse = this.From,
                 Product = item.Product,
                 SendoutNum = item.Count,
                 SendoutPrice = item.Price,
                 SendoutAmount = item.Count * item.Price,
             };

            if (fromFinancialStock.BusinessTime > fromLastFinancialStock.GetEndingBusinessTime())
            {
                fromFinancialStock.BusinessTime = fromLastFinancialStock.BusinessTime;
                fromFinancialStock.Sequence = fromLastFinancialStock.Sequence + 1;
            }

            fromFinancialStock.CrossMonthAccounting(context, fromLastFinancialStock, fromFollowUpFinancialStocks, fromProductStock);
            context.FinancialStocks.Add(fromFinancialStock);
            lastFinancialStockList.Add(fromFinancialStock);

            var toLastFinancialStock = lastFinancialStockList.FirstOrDefault(l => l.Product.Id == item.Product.Id && l.StoreHouse.Id == this.To.Id);
            if (toLastFinancialStock == null)
            {
                toLastFinancialStock = FinancialStock.GetCrossMonthLastBalance(item.Product, accountingTime.Year, accountingTime.Month
                , this.To, this.City, context);
            }
            else
            {
                lastFinancialStockList.Remove(toLastFinancialStock);
            }
            key = string.Format("{0}_{1}_{2}", this.City.Id, item.Product.Id, this.To.Id);
            List<FinancialStock> toFollowUpFinancialStocks;
            followUpFinancialStocksDic.TryGetValue(key, out toFollowUpFinancialStocks);
            if (toFollowUpFinancialStocks == null || toFollowUpFinancialStocks.Count == 0)
            {
                toFollowUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(toLastFinancialStock.BusinessTime, item.Product, this.To, this.City, context);
                followUpFinancialStocksDic.Add(key, toFollowUpFinancialStocks);
            }

            var toFinancialStock = new FinancialStock()
            {
                Type = FinancialStockType.物料调拨单,
                City = this.City,
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.NoteNo,
                BusinessItemId = this.Id,
                BusinessTime = toLastFinancialStock.BusinessTime.AddMilliseconds(2),
                StoreHouse = this.To,
                Product = item.Product,
                IncomeNum = item.Count,
                IncomePrice = item.Price,
                IncomeAmount = item.Count * item.Price,
            };
            if (toFinancialStock.BusinessTime > toLastFinancialStock.GetEndingBusinessTime())
            {
                toFinancialStock.BusinessTime = toLastFinancialStock.BusinessTime;
                toFinancialStock.Sequence = toLastFinancialStock.Sequence + 1;
            }

            toFinancialStock.CrossMonthAccounting(context, toLastFinancialStock, toFollowUpFinancialStocks, toProductStock);
            context.FinancialStocks.Add(toFinancialStock);
            lastFinancialStockList.Add(toFinancialStock);
        }

        public void Delete()
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("请先反审核！");

            this.IsDeleted = true;
        }
        */
        #endregion

    }
}
