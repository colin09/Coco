using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Model.PurchaseOutStockModule
{
    /// <summary>
    /// 采购入库单
    /// </summary>
    public class PurchaseOutStockNote : BaseEntity, IAggregationRoot, IDeleted
    {
        public PurchaseOutStockNote()
        {
            this.AuditState = NoteAuditState.待审核;
            this.IsDeleted = false;
            this.IsInternal = false;
        }

        /// <summary>
        /// 采购退货单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "审核状态")]
        public NoteAuditState AuditState { get; set; }

        [Display(Name = "审核备注")]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        [Display(Name = "出库日期")]
        public DateTime OutStockTime { get; set; }

        /// <summary>
        /// 是否已下推生成单据
        /// </summary>
        [Display(Name = "是否已下推生成单据")]
        public bool IsDownReason { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否内部单据
        /// </summary>
        public bool IsInternal { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        #region 聚合属性

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        [MaxLength(64)]
        [ForeignKey("SupplierCity")]
        public string SupplierCity_Id { get; set; }

        /// <summary>
        /// 供应城市
        /// </summary>
        public Org SupplierCity { get; set; }

        [MaxLength(64)]
        [ForeignKey("Supplier")]
        public string Supplier_Id { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Supplier { get; set; }

        [MaxLength(64)]
        [ForeignKey("OrgUser")]
        public string OrgUser_Id { get; set; }

        /// <summary>
        /// 录入人员
        /// </summary>
        public OrgUser OrgUser { get; set; }

        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }

        /// <summary>
        /// 出库仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }

        /// <summary>
        /// 出库明细
        /// </summary>
        public List<OutStockItem> Items { get; set; }

        #endregion

        //#region 业务方法

        ///// <summary>
        ///// 反审核
        ///// </summary>
        ///// <param name="context"></param>
        //public virtual void UnAudit(ERPContext context)
        //{
        //    if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
        //        throw new BusinessException("当前状态不能反审核");

        //    if (context.PayableBills.WhereNotDeleted().Any(b => b.PurchaseOutStockNote.Id == this.Id))
        //    {
        //        throw new BusinessException(string.Format("单据{0}无法反审核：请先删除应付单!", this.Id));
        //    }
        //    ProductLotManager lotManager = new ProductLotManager(context);
        //    List<ProductStock> list = new List<ProductStock>();
        //    ProductStock productStock;
        //    foreach (var item in Items)
        //    {
        //        productStock = list.FirstOrDefault(s => s.Product.Id == item.Product.Id &&
        //                                                s.StoreHouse.Id == item.StoreHouse.Id);
        //        if (productStock == null)
        //        {
        //            //处理库存 以及成本
        //            productStock = context.ProductStocks.Include("Product").Include("StoreHouse")
        //                .FirstOrDefault(s => s.Product.Id == item.Product.Id && s.StoreHouse.Id == item.StoreHouse.Id);
        //        }
        //        if (productStock != null)
        //        {
        //            productStock.AddStock(item.Num, item.Price);
        //            list.Add(productStock);
        //        }

        //        if (this.AuditState == NoteAuditState.已核算)
        //        {
        //            var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
        //                .FirstOrDefault(s => s.BusinessItemId == item.Id && s.Product.Id == item.Product.Id &&
        //                                     s.StoreHouse.Id == item.StoreHouse.Id);
        //            if (financialStock != null)
        //                financialStock.UnAudit(context);
        //        }
        //        lotManager.AddProductLot(item.StoreHouse, item.Product, item.Num);
        //    }

        //    this.AuditState = NoteAuditState.待审核;
        //    this.AuditRemark = "";

        //}

        //public virtual void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        //{
        //    if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
        //        throw new BusinessException("单据已审核");

        //    this.AuditState = auditState;
        //    this.AuditRemark = auditRemark;

        //    if (auditState == NoteAuditState.审核通过)
        //    {
        //        ProductLotManager lotManager = new ProductLotManager(context);
        //        List<ProductStock> productStockList = new List<ProductStock>();
        //        ProductStock productStock;

        //        DateTime now = DateTime.Now;
        //        List<FinancialStock> financialStockList = new List<FinancialStock>();
        //        FinancialStock financialStock;
        //        FinancialStock endingBalance;

        //        Dictionary<string, int> sequenceDic = new Dictionary<string, int>();

        //        foreach (var item in this.Items)
        //        {
        //            var key = this.StoreHouse_Id + "_" + item.Product.Id;
        //            if (sequenceDic.ContainsKey(key))
        //            {
        //                sequenceDic[key] += 1;
        //            }
        //            else
        //            {
        //                sequenceDic.Add(key, 0);
        //            }

        //            #region 库存处理

        //            HandleProductStock(context, productStockList, item, out productStock);

        //            #endregion

        //            endingBalance = financialStockList.FirstOrDefault(s => s.Product.Id == item.Product.Id
        //                                                                   && s.Year == now.Year && s.Month == now.Month
        //                                                                   && s.StoreHouse.Id == this.StoreHouse_Id
        //                                                                   && s.City.Id == this.City.Id);
        //            if (endingBalance == null)
        //            {
        //                endingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month,
        //                    this.StoreHouse, this.City, context);
        //                financialStockList.Add(endingBalance);
        //            }

        //            financialStock = GetFinancialStock(item, now, now, sequenceDic[key]);

        //            financialStock.Accounting(context, endingBalance, productStock);
        //            context.FinancialStocks.Add(financialStock);
        //            lotManager.ReduceProductLot(this.StoreHouse, item.Product, item.Num);
        //        }

        //        this.AuditState = NoteAuditState.已核算;
        //    }
        //}

        //public virtual void CrossMonthAuditAndAccounting(ERPContext context, NoteAuditState auditState,
        //    string auditRemark)
        //{
        //    if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
        //        throw new BusinessException("单据已审核");

        //    this.AuditState = auditState;
        //    this.AuditRemark = auditRemark;

        //    if (auditState == NoteAuditState.审核通过)
        //    {
        //        ProductLotManager lotManager = new ProductLotManager(context);
        //        //缓存所有 Item设计的 产品库存
        //        List<ProductStock> productStockList = new List<ProductStock>();

        //        //缓存所有 Item的最后一条存货收发存明细 用于计算结存并避免出现重复物料造成的 多次计算问题
        //        List<FinancialStock> lastFinancialStockList = new List<FinancialStock>();

        //        //缓存 所有Item 结算 所影响的 存货收发存明细
        //        Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic =
        //            new Dictionary<string, List<FinancialStock>>(this.Items.Count);

        //        ProductStock productStock; //临时变量——产品库存

        //        foreach (var item in this.Items)
        //        {

        //            //库存处理
        //            HandleProductStock(context, productStockList, item, out productStock);

        //            //核算
        //            HandleCrossMonthAccounting(item, followUpFinancialStocksDic, lastFinancialStockList, productStock,
        //                context);

        //            lotManager.ReduceProductLot(this.StoreHouse, item.Product, item.Num);
        //        }

        //        this.AuditState = NoteAuditState.已核算;
        //    }
        //}

        //private void HandleProductStock(ERPContext context, List<ProductStock> productStockList, OutStockItem item,
        //    out ProductStock productStock)
        //{
        //    productStock =
        //        productStockList.FirstOrDefault(p => p.Product.Id == item.Product.Id &&
        //                                             p.StoreHouse.Id == item.StoreHouse.Id);
        //    if (productStock == null)
        //    {
        //        //同步库存
        //        productStock = context.ProductStocks.Include("Product").Include("StoreHouse").FirstOrDefault(p =>
        //            p.Product.Id == item.Product.Id
        //            && p.StoreHouse.Id == item.StoreHouse.Id);
        //    }

        //    if (productStock == null)
        //        throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", item.StoreHouse.Name,
        //            item.Product.Info.Desc.ProductName));
        //    productStock.ReduceStock(item.Num, item.Price);
        //    productStockList.Add(productStock);
        //}

        //private void HandleCrossMonthAccounting(OutStockItem item,
        //    Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic,
        //    List<FinancialStock> lastFinancialStockList, ProductStock productStock, ERPContext context)
        //{
        //    var accountingTime = this.OutStockTime;

        //    var lastFinancialStock =
        //        lastFinancialStockList.FirstOrDefault(l => l.Product.Id == item.Product.Id &&
        //                                                   l.StoreHouse.Id == item.StoreHouse.Id);
        //    if (lastFinancialStock == null)
        //    {
        //        lastFinancialStock = FinancialStock.GetCrossMonthLastBalance(item.Product, accountingTime.Year,
        //            accountingTime.Month
        //            , item.StoreHouse, this.City, context);
        //    }
        //    else
        //    {
        //        lastFinancialStockList.Remove(lastFinancialStock);
        //    }

        //    string key = string.Format("{0}_{1}_{2}", this.City.Id, item.Product.Id, item.StoreHouse.Id);
        //    List<FinancialStock> followUpFinancialStocks;
        //    followUpFinancialStocksDic.TryGetValue(key, out followUpFinancialStocks);
        //    if (followUpFinancialStocks == null || followUpFinancialStocks.Count == 0)
        //    {
        //        followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime,
        //            item.Product, item.StoreHouse, this.City, context);
        //        followUpFinancialStocksDic.Add(key, followUpFinancialStocks);
        //    }

        //    var financialStock = GetFinancialStock(item, this.StoreHouse, accountingTime, lastFinancialStock);

        //    financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks, productStock);
        //    context.FinancialStocks.Add(financialStock);
        //    lastFinancialStockList.Add(financialStock);
        //}

        //private FinancialStock GetFinancialStock(OutStockItem item, StoreHouse storeHouse, DateTime accountingTime,
        //    FinancialStock lastFinancialStock)
        //{
        //    var financialStock = new FinancialStock()
        //    {
        //        Type = FinancialStockType.采购退货单,
        //        City = this.City,
        //        Year = accountingTime.Year,
        //        Month = accountingTime.Month,
        //        BusinessId = this.NoteNo,
        //        BusinessItemId = item.Id,
        //        BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
        //        StoreHouse = storeHouse,
        //        Product = item.Product,
        //        IncomeNum = 0 - item.Num,
        //        IncomePrice = item.Price,
        //        IncomeAmount = 0 - item.TotalAmount,
        //    };

        //    if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
        //    {
        //        financialStock.BusinessTime = lastFinancialStock.BusinessTime;
        //        financialStock.Sequence = lastFinancialStock.Sequence + 1;
        //    }

        //    return financialStock;
        //}

        //private FinancialStock GetFinancialStock(OutStockItem item, StoreHouse storeHouse, DateTime accountingTime,
        //    DateTime businessTime, int sequence = 0)
        //{
        //    var financialStock = new FinancialStock()
        //    {
        //        Type = FinancialStockType.采购退货单,
        //        City = this.City,
        //        Year = accountingTime.Year,
        //        Month = accountingTime.Month,
        //        BusinessId = this.NoteNo,
        //        BusinessItemId = item.Id,
        //        BusinessTime = businessTime,
        //        Sequence = sequence,
        //        StoreHouse = storeHouse,
        //        Product = item.Product,
        //        IncomeNum = 0 - item.Num,
        //        IncomePrice = item.Price,
        //        IncomeAmount = 0 - item.TotalAmount,
        //    };
        //    return financialStock;
        //}

        //public virtual void Delete(ERPContext context)
        //{
        //    if (this.IsDeleted)
        //        return;
        //    if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
        //        throw new BusinessException("请先反审核！");

        //    if (context.PayableBills.WhereNotDeleted().Any(b => b.PurchaseOutStockNote.Id == this.Id))
        //        throw new BusinessException("请先删除应付单！");

        //    this.IsDeleted = true;
        //}

        //#endregion


        public void ValidateUnAuditState()
        {
            if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (this.IsDownReason)
            {
                throw new BusinessException(string.Format("单据{0}无法反审核：请先删除应付单!", this.Id));
            }

        }
    }
}
