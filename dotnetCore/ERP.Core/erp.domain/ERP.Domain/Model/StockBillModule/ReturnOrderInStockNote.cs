using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.StockBillModule
{
    public class ReturnOrderInStockNote : BaseEntity, IAggregationRoot, IDeleted
    {
        public ReturnOrderInStockNote()
        {
            this.AuditState = NoteAuditState.待审核;
            this.IsDownReason = false;
            this.IsInternal = false;
        }

        #region 基本属性

        /// <summary>
        /// 销售退货单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 退货订单编号
        /// </summary>
        [MaxLength(64)]
        public string ReturnOrderNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public NoteAuditState AuditState { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [MaxLength(200)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 是否已下推生成单据
        /// </summary>
        public bool IsDownReason { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 是否内部单据
        /// </summary>
        public bool IsInternal { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
        [NotMapped]
        public string CityName { get; set; }
        #endregion

        #region 聚合属性
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        [ForeignKey("SaleCity")]
        [MaxLength(64)]
        public string SaleCity_Id { get; set; }

        /// <summary>
        /// 销售城市
        /// </summary>
        public City SaleCity { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }
        [ForeignKey("Order")]
        [MaxLength(64)]
        public string Order_Id { get; set; }
        public Order Order { get; set; }
        [ForeignKey("ReturnOrder")]
        [MaxLength(64)]
        public string ReturnOrder_Id { get; set; }
        public ReturnOrder ReturnOrder { get; set; }
        [ForeignKey("User")]
        [MaxLength(64)]
        public string User_Id { get; set; }
        public User User { get; set; }
        [ForeignKey("CompanyUserCity")]
        [MaxLength(64)]
        public string CompanyUserCity_Id { get; set; }
        public Org CompanyUserCity { get; set; }
        [ForeignKey("BrokerUser")]
        [MaxLength(64)]
        public string BrokerUser_Id { get; set; }
        public OrgUser BrokerUser { get; set; }

        public List<ReturnNoteItem> Items { get; set; }

        #endregion

        #region 方法
        /*
        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="context"></param>
        public virtual void UnAudit(ERPContext context)
        {
            if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (context.ReceivableBills.WhereNotDeleted().Any(b => b.ReturnOrderInStockNote.Id == this.Id))
                throw new BusinessException(string.Format("单据{0}不能反审核,请先删除应收单！", this.Id));
            ProductLotManager lotManager = new ProductLotManager(context);
            foreach (var item in Items)
            {
                //处理库存 以及成本
                var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info").FirstOrDefault(s => s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                if (productStock != null)
                {
                    productStock.ReduceStock(item.Num, item.Price);
                }

                if (this.AuditState == NoteAuditState.已核算)
                {
                    var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
                        .FirstOrDefault(s => (s.BusinessItemId == this.Id || s.BusinessItemId == item.Id) && s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                    if (financialStock != null)
                        financialStock.UnAudit(context);
                }

                lotManager.ReduceProductLot(this.StoreHouse, item.Product, item.Num);
            }

            this.AuditState = NoteAuditState.待审核;
            this.AuditRemark = "";
        }

        public virtual void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
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
                ProductStock productStock;

                DateTime now = DateTime.Now;
                List<FinancialStock> financialStockList = new List<FinancialStock>();
                FinancialStock financialStock;
                FinancialStock endingBalance;

                Dictionary<string, int> sequenceDic = new Dictionary<string, int>();

                foreach (var item in this.Items)
                {
                    var key = item.Product.Id;
                    if (sequenceDic.ContainsKey(key))
                    {
                        sequenceDic[key] += 1;
                    }
                    else
                    {
                        sequenceDic.Add(key, 0);
                    }
                    //处理库存
                    HandleProductStock(context, productStockList, item, out productStock);

                    endingBalance = financialStockList.FirstOrDefault(s => s.Product.Id == item.Product.Id
                                      && s.Year == now.Year && s.Month == now.Month
                                      && s.StoreHouse.Id == this.StoreHouse.Id
                                      && s.City.Id == this.StoreHouse.City.Id);
                    if (endingBalance == null)
                    {
                        endingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);
                        financialStockList.Add(endingBalance);
                    }

                    financialStock = GetFinancialStock(item, endingBalance, now, now, sequenceDic[key]);

                    financialStock.Accounting(context, endingBalance, productStock);
                    context.FinancialStocks.Add(financialStock);

                    if (this.ReturnOrder == null)
                    {
                        item.CostPrice = endingBalance.BalancePrice;
                    }
                    item.LastUpdateTime = DateTime.Now;
                    lotManager.AddProductLot(this.StoreHouse, item.Product, item.Num);
                }

                this.AuditState = NoteAuditState.已核算;
            }
        }

        public virtual void CrossMonthAuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
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

                ProductStock productStock;//临时变量——产品库存

                foreach (var item in this.Items)
                {
                    //处理库存
                    HandleProductStock(context, productStockList, item, out productStock);

                    //核算
                    HandleCrossMonthAccounting(item, followUpFinancialStocksDic, lastFinancialStockList, productStock, context);

                    lotManager.AddProductLot(this.StoreHouse, item.Product, item.Num);
                }
                this.AuditState = NoteAuditState.已核算;
            }
        }

        private void HandleProductStock(ERPContext context, List<ProductStock> productStockList, ReturnNoteItem item, out ProductStock productStock)
        {
            productStock = context.ProductStocks.FirstOrDefault(s => s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);

            if (productStock == null)
            {
                productStock = new ProductStock()
                {
                    City_Id = this.City.Id,
                    City = this.City,
                    StoreHouse = this.StoreHouse,
                    Product = item.Product,
                    StockUnit = item.Unit,
                    PriceUnit = item.Unit,
                    CostPrice = item.Price,
                    StockNum = 0,
                };
                context.ProductStocks.Add(productStock);
            }

            productStock.AddStock(item.Num, item.Price);
            productStockList.Add(productStock);
        }
        private void HandleCrossMonthAccounting(ReturnNoteItem item, Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic, List<FinancialStock> lastFinancialStockList, ProductStock productStock, ERPContext context)
        {
            DateTime accountingTime = this.BusinessTime;

            //当前产品、仓库 结算月最后一条 存货收发明细
            var lastFinancialStock = lastFinancialStockList.FirstOrDefault(l => l.Product.Id == item.Product.Id && l.StoreHouse.Id == this.StoreHouse.Id);
            if (lastFinancialStock == null)
            {
                lastFinancialStock = FinancialStock.GetCrossMonthLastBalance(item.Product, accountingTime.Year, accountingTime.Month
                , this.StoreHouse, this.StoreHouse.City, context);
            }
            else
            {
                lastFinancialStockList.Remove(lastFinancialStock);
            }

            string key = string.Format("{0}_{1}_{2}", this.StoreHouse.City.Id, item.Product.Id, this.StoreHouse.Id);
            List<FinancialStock> followUpFinancialStocks;//当前产品、仓库  结算时间后 所有收发明细（包含期初、期末）
            followUpFinancialStocksDic.TryGetValue(key, out followUpFinancialStocks);
            if (followUpFinancialStocks == null || followUpFinancialStocks.Count == 0)
            {
                followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime, item.Product, this.StoreHouse, this.StoreHouse.City, context);
                followUpFinancialStocksDic.Add(key, followUpFinancialStocks);
            }

            var financialStock = GetFinancialStock(item, lastFinancialStock, accountingTime, lastFinancialStock);

            financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks, productStock);
            context.FinancialStocks.Add(financialStock);
            lastFinancialStockList.Add(financialStock);

            if (this.ReturnOrder == null)
            {
                item.CostPrice = lastFinancialStock.BalancePrice;
            }
            item.LastUpdateTime = DateTime.Now;
        }

        private FinancialStock GetFinancialStock(ReturnNoteItem item, FinancialStock endingBalance, DateTime accountingTime, DateTime businessTime, int sequence = 0)
        {
            //说明：退货入库单  成本核算时 的成本价  是销售出库单 成本核算时的成本价
            var financialStock = new FinancialStock()
            {
                Type = FinancialStockType.退货入库单,
                City = this.StoreHouse.City,
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.NoteNo,
                BusinessItemId = item.Id,
                BusinessTime = businessTime,
                Sequence = sequence,
                StoreHouse = this.StoreHouse,
                Product = item.Product,
                //IncomeNum = item.Num,
                //IncomePrice = this.Order == null ? endingBalance.BalancePrice : item.CostPrice,
                //IncomeAmount = this.Order == null ? item.Num * endingBalance.BalancePrice : item.Num * item.CostPrice,
                SendoutNum = 0 - item.Num,
                SendoutPrice = this.Order == null ? endingBalance.BalancePrice : item.CostPrice,
                SendoutAmount = 0 - (this.Order == null ? item.Num * endingBalance.BalancePrice : item.Num * item.CostPrice),
            };

            return financialStock;
        }
        private FinancialStock GetFinancialStock(ReturnNoteItem item, FinancialStock endingBalance, DateTime accountingTime, FinancialStock lastFinancialStock)
        {
            //说明：退货入库单  成本核算时 的成本价  是销售出库单 成本核算时的成本价
            var financialStock = new FinancialStock()
            {
                Type = FinancialStockType.退货入库单,
                City = this.StoreHouse.City,
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.NoteNo,
                BusinessItemId = item.Id,
                BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
                StoreHouse = this.StoreHouse,
                Product = item.Product,
                SendoutNum = 0 - item.Num,
                SendoutPrice = this.Order == null ? endingBalance.BalancePrice : item.CostPrice,
                SendoutAmount = 0 - (this.Order == null ? item.Num * endingBalance.BalancePrice : item.Num * item.CostPrice),
            };

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            return financialStock;
        }

        public virtual void Delete(ERPContext context)
        {
            if (this.IsDeleted)
                return;
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            if (this.ReturnOrder != null && this.ReturnOrder.Order != null)
            {
                if (context.ReceivableBills.WhereNotDeleted().Any(b => b.OrderNo == this.ReturnOrder.Order.OrderNo && b.AdValoremAmount < 0))
                    throw new BusinessException("请先删除应收单！");
                this.ReturnOrder.HasCreateReturnNote = false;
            }

            this.IsDeleted = true;
        }*/
        #endregion
   
    }
}
