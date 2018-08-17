using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ERP.Domain.Model.AuditTraceModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.CommonModule;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Domain.Utility;
using ERP.Domain.Enums.PurchaseInStockModule;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.AuditTraceModule;
using ERP.Domain.Model.ProductStockModule;

namespace ERP.Domain.Model.PurchaseInStockModule
{
    /// <summary>
    /// 采购入库单
    /// </summary>
    public class PurchaseInStockNote : BaseEntity, IAggregationRoot, IDeleted, IHidden, ICannotEditAndDelete, INotSyncStock, IHasCreateRelatedNote
    {
        public PurchaseInStockNote()
        {
            this.AuditState = PurchaseInStockNoteAuditState.待采购审核;
            this.IsDeleted = false;
            this.IsInternal = false;
            this.AuditTraces = new List<PurchaseInStockNoteAuditTrace>();
            this.Items = new List<InStockItem>();
        }

        #region 基本属性
        /// <summary>
        /// 采购类型
        /// </summary>
        public PurchaseBusinessType PurchaseBusinessType { get; set; }

        public int Version { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PurchaseInStockNoteAuditState AuditState { get; set; }

        /// <summary>
        /// 是否已创建关联单据
        /// </summary>
        public bool HasCreateRelatedNote { get; set; }

        [MaxLength(256)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        [Display(Name = "入库日期")]
        public DateTime InStockTime { get; set; }
        /// <summary>
        /// 生成异常入库单的时间
        /// </summary>
        [Display(Name = "审核时间")]
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 装卸费
        /// </summary>
        public decimal HandlingCost { get; set; }

        /// <summary>
        /// 物流费
        /// </summary>
        public decimal LogisticsFee { get; set; }

        /// <summary>
        /// 是否已下推生成单据
        /// </summary>
        public bool IsDownReason { get; set; }

        /// <summary>
        /// 是否不可编辑和删除
        /// </summary>
        public bool CannotEditAndDelete { get; set; }

        /// <summary>
        /// 是否不同步库存
        /// </summary>
        public bool IsNotSyncStock { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否隐藏数据
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 是否内部单据
        /// </summary>
        [Display(Name = "是否集团内部采购")]
        public bool IsInternal { get; set; }

        /// <summary>
        /// 是否来源于代销售单据
        /// </summary>
        public bool IsFromAgencySaleNote { get; set; }

        /// <summary>
        /// 是否需要采购审核（需要采购审核的：根据采购申请录入的入库单； 供应商-城市、城市-城市、总部-城市：调入城市入库单；）
        /// </summary>
        public bool NeedPurchaseAudit { get; set; }

        /// <summary>
        /// 下一个需要审核的角色（异常审核状态才有值，应与AuditTraces中最后一条待审核记录同步）
        /// </summary>
        [MaxLength(256)]
        public string NeedAuditUserRole { get; set; }

        /// <summary>
        /// 是否来源于 城市调拨单
        /// </summary>
        [Obsolete("待删除字段")]
        public bool IsFromCityAllocation { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 采购批次编号
        /// </summary>
        [MaxLength(64)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 采购单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 采购Id
        /// </summary>
        [MaxLength(64)]
        public string PurchaseUserId { get; set; }
        /// <summary>
        /// 采购人员
        /// </summary>
        [MaxLength(32)]
        public string PurchaseName { get; set; }
        /// <summary>
        /// 采购联系方式
        /// </summary>
        [MaxLength(32)]
        public string PurchaseMobileNo { get; set; }

        /// <summary>
        /// 是否短信通知
        /// </summary>
        public bool WhetherSmsNotify { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [MaxLength(50)]
        public string Operators { get; set; }

        /// <summary>
        /// 核销日期(月为单位)
        /// </summary>
        public int VerificationDate { get; set; }

        /// <summary>
        /// 返利总额
        /// </summary>
        public decimal RebateTotalAmount { get; set; }

        /// <summary>
        /// 是否创建应收返利单
        /// </summary>
        public bool IsCreateRebateBill { get; set; }


        #region 冗余字段

        /// <summary>
        /// 采购申请单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseRequisitionNo { get; set; }

        /// <summary>
        /// 关联的调拨单编号
        /// </summary>
        [MaxLength(64)]
        public string AllocationNoteNo { get; set; }

        #endregion

        #endregion

        #region 聚合属性
        [ForeignKey("PurchaseRequisition")]
        [MaxLength(64)]
        public string PurchaseRequisition_Id { get; set; }
        /// <summary>
        /// 采购申请
        /// </summary>
        public PurchaseRequisition PurchaseRequisition { get; set; }

        [ForeignKey("AllocationNote")]
        [MaxLength(64)]
        public string AllocationNote_Id { get; set; }
        /// <summary>
        /// 调拨单
        /// </summary>
        public AllocationNote AllocationNote { get; set; }

        public List<PurchaseInStockNoteAuditTrace> AuditTraces { get; set; }
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }

        public StoreHouse StoreHouse { get; set; }

        [ForeignKey("SupplierCity")]
        [MaxLength(64)]
        public string SupplierCity_Id { get; set; }
        /// <summary>
        /// 供应城市
        /// </summary>
        public Org SupplierCity { get; set; }

        [ForeignKey("Supplier")]
        [MaxLength(64)]
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

        /// <summary>
        /// 入库明细
        /// </summary>
        public List<InStockItem> Items { get; set; }

        #endregion

        #region 业务方法
        /*
        public virtual void UnAudit(ERPContext context)
        {
            if (this.AuditState != PurchaseInStockNoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (context.PayableBills.WhereNotDeleted().Any(b => b.PurchaseInStockNote.Id == this.Id))
            {
                throw new BusinessException("请先删除应付单!");
            }

            List<ProductStock> list = new List<ProductStock>();
            ProductStock productStock;
            ProductLotManager lotManager = new ProductLotManager(context);
            foreach (var item in Items)
            {
                //处理库存 以及成本
                productStock = list.FirstOrDefault(p => p.Product.Id == item.Product.Id && p.StoreHouse.Id == this.StoreHouse.Id);
                if (productStock == null)
                {
                    productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info").FirstOrDefault(s => s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                }
                if (productStock != null)
                {
                    productStock.ReduceStock(item.Num, item.Price);
                    list.Add(productStock);
                }

                if (this.AuditState == PurchaseInStockNoteAuditState.已核算)
                {
                    var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
                        .FirstOrDefault(s => s.BusinessItemId == item.Id && s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                    if (financialStock != null)
                    {
                        financialStock.UnAudit(context);
                    }
                }

                lotManager.ReduceProductLot(this, item);
            }

            this.AuditState = PurchaseInStockNoteAuditState.待财务审核;
            this.AuditRemark = "";
        }

        public virtual void AuditAndAccounting(ERPContext context, PurchaseInStockNoteAuditState auditState, string auditRemark)
        {
            ValidateAuditState(auditState);

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == PurchaseInStockNoteAuditState.已核算)
            {
                this.Version++;

                ProductStock productStock;
                List<ProductStock> productStockList = new List<ProductStock>();

                DateTime now = DateTime.Now;
                FinancialStock financialStock;
                List<FinancialStock> endingFinancialStockList = new List<FinancialStock>();
                FinancialStock endingBalance;
                Dictionary<string, int> sequenceDic = new Dictionary<string, int>();
                ProductLotManager lotManager = new ProductLotManager(context);

                foreach (var item in this.Items)
                {
                    var key = this.StoreHouse.Id + "_" + item.Product.Id;
                    if (sequenceDic.ContainsKey(key))
                    {
                        sequenceDic[key] += 1;
                    }
                    else
                    {
                        sequenceDic.Add(key, 0);
                    }

                    HandleProductStock(context, productStockList, item, out productStock);

                    endingBalance = endingFinancialStockList.FirstOrDefault(s => s.Product.Id == item.Product.Id
                    && s.Year == now.Year && s.Month == now.Month
                    && s.StoreHouse.Id == this.StoreHouse.Id
                    && s.City.Id == this.City.Id);
                    if (endingBalance == null)
                    {
                        endingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month, this.StoreHouse, this.City, context);
                        endingFinancialStockList.Add(endingBalance);
                    }
                    financialStock = new FinancialStock()
                    {
                        Type = FinancialStockType.采购入库单,
                        City = this.City,
                        Year = now.Year,
                        Month = now.Month,
                        BusinessId = this.NoteNo,
                        BusinessItemId = item.Id,
                        BusinessTime = now,
                        Sequence = sequenceDic[key],
                        StoreHouse = this.StoreHouse,
                        Product = item.Product,
                        IncomeNum = item.Num,
                        IncomePrice = item.Price,
                        IncomeAmount = item.TotalAmount,
                    };
                    financialStock.Accounting(context, endingBalance, productStock);
                    context.FinancialStocks.Add(financialStock);
                    lotManager.AddProductLot(this, item);
                }
                this.AuditState = PurchaseInStockNoteAuditState.已核算;
            }
        }

        public virtual void CrossMonthAuditAndAccounting(ERPContext context, PurchaseInStockNoteAuditState auditState, string auditRemark)
        {
            ValidateAuditState(auditState);

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == PurchaseInStockNoteAuditState.已核算)
            {
                this.Version++;
                //缓存所有 Item设计的 产品库存
                List<ProductStock> productStockList = new List<ProductStock>();
                //缓存所有 Item的最后一条存货收发存明细 用于计算结存并避免出现重复物料造成的 多次计算问题
                List<FinancialStock> lastFinancialStockList = new List<FinancialStock>();
                //缓存 所有Item 结算 所影响的 存货收发存明细
                Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic = new Dictionary<string, List<FinancialStock>>(this.Items.Count);
                ProductStock productStock;//临时变量——产品库存
                ProductLotManager lotManager = new ProductLotManager(context);

                foreach (var item in this.Items)
                {
                    //库存处理
                    HandleProductStock(context, productStockList, item, out productStock);
                    //核算
                    HandleCrossMonthAccounting(item, followUpFinancialStocksDic, lastFinancialStockList, productStock, context);
                    //处理产品批次库存
                    lotManager.AddProductLot(this, item);
                }
                this.AuditState = PurchaseInStockNoteAuditState.已核算;


            }
        }

        public void ValidateAuditState(PurchaseInStockNoteAuditState auditState)
        {
            if (this.AuditState == PurchaseInStockNoteAuditState.已核算)
                throw new BusinessException("单据已审核");

            if (this.AuditState == PurchaseInStockNoteAuditState.财务拒绝 && auditState == PurchaseInStockNoteAuditState.财务拒绝)
            {
                throw new BusinessException("当前状态已为财务拒绝状态，请不要重复操作！");
            }

            if (!string.IsNullOrWhiteSpace(this.PurchaseRequisition_Id) &&
                (this.AuditState != PurchaseInStockNoteAuditState.待财务审核
                && this.AuditState != PurchaseInStockNoteAuditState.异常拒绝))
            {
                throw new BusinessException("当前状态不可审核！");
            }
        }

        public void ValidateUnAuditState()
        {
            if (this.AuditState != PurchaseInStockNoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (this.IsDownReason)
            {
                throw new BusinessException("请先删除应付单!");
            }
            if (this.IsCreateRebateBill)
            {
                throw new BusinessException("请先删除返利应收单!");
            }
        }

        private void HandleProductStock(ERPContext context, List<ProductStock> productStockList, InStockItem item, out ProductStock productStock)
        {
            productStock = productStockList.FirstOrDefault(p => p.Product.Id == item.Product.Id && p.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
            {
                //同步库存
                productStock = context.ProductStocks.Include("Product").Include("StoreHouse").FirstOrDefault(p =>
                    p.Product.Id == item.Product.Id
                    && p.StoreHouse.Id == this.StoreHouse.Id);
            }
            if (productStock == null)
            {
                productStock = new ProductStock()
                {
                    City_Id = this.City.Id,
                    City = this.City,
                    Product = item.Product,
                    StockNum = item.Num,
                    StockUnit = item.Unit,
                    CostPrice = item.Price,
                    PriceUnit = item.Unit,
                    StoreHouse = this.StoreHouse,
                };
                context.ProductStocks.Add(productStock);
            }
            else
            {
                productStock.AddStock(item.Num, item.Price);
            }
            productStockList.Add(productStock);
        }
        private void HandleCrossMonthAccounting(InStockItem item, Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic, List<FinancialStock> lastFinancialStockList, ProductStock productStock, ERPContext context)
        {
            var accountingTime = this.InStockTime;

            var lastFinancialStock = lastFinancialStockList.FirstOrDefault(l => l.Product.Id == item.Product.Id && l.StoreHouse.Id == this.StoreHouse.Id);
            if (lastFinancialStock == null)
            {
                lastFinancialStock = FinancialStock.GetCrossMonthLastBalance(item.Product, accountingTime.Year, accountingTime.Month
                , this.StoreHouse, this.City, context);
            }
            else
            {
                lastFinancialStockList.Remove(lastFinancialStock);
            }

            string key = string.Format("{0}_{1}_{2}", this.City.Id, item.Product.Id, this.StoreHouse.Id);
            List<FinancialStock> followUpFinancialStocks;
            followUpFinancialStocksDic.TryGetValue(key, out followUpFinancialStocks);
            if (followUpFinancialStocks == null || followUpFinancialStocks.Count == 0)
            {
                followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime, item.Product, this.StoreHouse, this.City, context);
                followUpFinancialStocksDic.Add(key, followUpFinancialStocks);
            }

            var financialStock = new FinancialStock()
             {
                 Type = FinancialStockType.采购入库单,
                 City = this.City,
                 Year = accountingTime.Year,
                 Month = accountingTime.Month,
                 BusinessId = this.NoteNo,
                 BusinessItemId = item.Id,
                 BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
                 StoreHouse = this.StoreHouse,
                 Product = item.Product,
                 IncomeNum = item.Num,
                 IncomePrice = item.Price,
                 IncomeAmount = item.TotalAmount,
             };

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks, productStock);
            context.FinancialStocks.Add(financialStock);
            lastFinancialStockList.Add(financialStock);
        }

        public virtual void CheckDelete()
        {
            if (this.IsDeleted)
                return;

            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可删除！");

            if (this.PurchaseBusinessType == PurchaseBusinessType.代理采购
                && this.HasCreateRelatedNote)
                throw new BusinessException("已生成调拨单的代理采购单不可删除！");

            if (this.AuditState == PurchaseInStockNoteAuditState.异常拒绝
                || this.AuditState == PurchaseInStockNoteAuditState.异常审核)
                throw new BusinessException("当前状态不可删除！");

            if ((!string.IsNullOrWhiteSpace(this.PurchaseRequisition_Id) || !string.IsNullOrWhiteSpace(this.AllocationNote_Id))
                && this.AuditState == PurchaseInStockNoteAuditState.待财务审核)
                throw new BusinessException("当前状态不可删除！");

            if (this.AuditState == PurchaseInStockNoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            if (this.IsDownReason)
                throw new BusinessException("请先删除应付单！");

            if (this.IsCreateRebateBill)
            {
                throw new BusinessException("请先删除返利应收单!");
            }
        }
        /// <summary>
        /// 编辑检查
        /// </summary>
        public virtual void EditCheckForRequisitionInStockNote()
        {
            if (this.AuditState != PurchaseInStockNoteAuditState.待采购审核
                && this.AuditState != PurchaseInStockNoteAuditState.采购拒绝
                && this.AuditState != PurchaseInStockNoteAuditState.财务拒绝)
            {
                throw new BusinessException("只有待采购审核或审核拒绝的单据可修改！");
            }
            if (this.IsFromAgencySaleNote)
            {
                throw new BusinessException("代销售单据产生的入库单，不可编辑！");
            }
            if (this.PurchaseBusinessType == PurchaseBusinessType.代理采购 && this.HasCreateRelatedNote)
            {
                throw new BusinessException("代理采购单已生成关联单据不可编辑！");
            }
            if (this.CannotEditAndDelete)
            {
                throw new BusinessException("该单据不可编辑！");
            }
            if (string.IsNullOrWhiteSpace(this.PurchaseRequisition_Id))
            {
                throw new BusinessException("采购入库单没有对应的采购申请单！");
            }
        }

        public virtual void EditCheckForAllocationNote()
        {
            if (string.IsNullOrWhiteSpace(this.AllocationNote_Id))
                throw new BusinessException("调拨单不存在！");

            if (this.AuditState != PurchaseInStockNoteAuditState.待采购审核
                   && this.AuditState != PurchaseInStockNoteAuditState.采购拒绝
                   && this.AuditState != PurchaseInStockNoteAuditState.财务拒绝)
            {
                throw new BusinessException("只有待采购审核或审核拒绝的单据可修改！");
            }
            if (this.IsFromAgencySaleNote)
            {
                throw new BusinessException("代销售单据产生的入库单，不可编辑！");
            }
            if (this.PurchaseBusinessType == PurchaseBusinessType.代理采购)
            {
                throw new BusinessException("代理采购单不可编辑！");
            }
            if (this.CannotEditAndDelete)
            {
                throw new BusinessException("该单据不可编辑！");
            }
        }

        public virtual void CheckEdit()
        {
            if (!string.IsNullOrWhiteSpace(this.PurchaseRequisition_Id))
            {
                throw new BusinessException("采购申请生成的入库单不可调用该方法！");
            }
            if (!string.IsNullOrWhiteSpace(this.AllocationNote_Id))
            {
                throw new BusinessException("调拨单生成的入库单不可调用该方法！");
            }

            if (this.AuditState != PurchaseInStockNoteAuditState.待采购审核
                  && this.AuditState != PurchaseInStockNoteAuditState.采购拒绝
                  && this.AuditState != PurchaseInStockNoteAuditState.财务拒绝)
            {
                throw new BusinessException("只有待采购审核或审核拒绝的单据可修改！");
            }
            if (this.IsFromAgencySaleNote)
            {
                throw new BusinessException("代销售单据产生的入库单，不可编辑！");
            }
            if (this.PurchaseBusinessType == PurchaseBusinessType.代理采购)
            {
                throw new BusinessException("代理采购单不可编辑！");
            }
            if (this.CannotEditAndDelete)
            {
                throw new BusinessException("该单据不可编辑！");
            }
        }

        /// <summary>
        /// 分摊 装卸费和物流费
        /// 分摊规则：根据大单位数量分摊； 入库单项都不足1件的，根据小单位数量分摊；赠品分摊后 变为非赠品；
        /// </summary>
        public virtual void ShareHandlingCostAndLogisticsFee()
        {
            decimal totalAmount = this.HandlingCost + this.LogisticsFee;
            if (totalAmount == 0) return;

            if (this.Items == null || this.Items.Count == 0 || this.Items.Any(i => i.Product == null || i.Product.Info == null))
                throw new BusinessException("未加载采购入库单项信息，无法分摊费用！");


            bool shareByUnitCount = false;//根据小单位数量分摊？
            Func<InStockItem, int> countQuery = i => i.Num / i.Product.Info.Desc.PackageQuantity;

            if (this.Items.Count(i => i.Num / i.Product.Info.Desc.PackageQuantity > 0) == 0)
            {
                countQuery = i => i.Num;
                shareByUnitCount = true;
            }
            //总分摊数量
            int totalCount = this.Items.Sum(countQuery);
            int remainingCount = totalCount;
            decimal remainingAmount = totalAmount;//分摊剩下的金额
            foreach (var item in this.Items)
            {
                int count = shareByUnitCount ? item.Num : item.Num / item.Product.Info.Desc.PackageQuantity;//订单项分摊数量
                if (count == 0) continue;

                decimal shareAmount;
                if (remainingCount == count)
                    shareAmount = remainingAmount;
                else
                    shareAmount = Math.Round(count * totalAmount / totalCount, 2);//分摊金额

                if (shareAmount > decimal.Zero)
                {
                    item.TotalAmount += shareAmount;
                    item.Price = item.TotalAmount / item.Num;
                    item.BarePrice = (item.TotalAmount - item.RebateTotalAmount) / item.Num;
                }
                remainingCount -= count;
                remainingAmount -= shareAmount;
            }

            if (remainingAmount != decimal.Zero)
                throw new BusinessException("装卸费、物流费分摊异常！");
        }

        /// <summary>
        /// 审核
        ///   .Include("Items.Product.Info")
        ///   .Include("Items.AllocationNoteItem")
        ///   .Include("PurchaseRequisition.Items.Product")
        ///   .Include("AllocationNote.Items")
        ///   .Include("AllocationNote.ToCity")
        ///   .Include("AuditTraces")
        ///   .Include("City")
        ///   .Include("Items.StoreHouse")
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userMobileNo"></param>
        /// <param name="userTrueName"></param>
        /// <param name="userRole"></param>
        /// <param name="remark"></param>
        /// <param name="handlingCost"></param>
        /// <param name="logisticsFee"></param>
        /// <param name="passed"></param>
        public virtual void Audit(string userId, string userMobileNo, string userTrueName, string userRole, string remark, decimal handlingCost, decimal logisticsFee, bool passed)
        {

            if (passed)
            {//更新关联入库单的调拨单或者采购申请单的入库数量，更改入库单状态为待财务审核
                if (this.AllocationNote != null && this.AllocationNote.ToCity.Id == this.City.Id)
                {
                    AllocationNoteItem allocationNoteItem;
                    foreach (var item in this.Items)
                    {
                        allocationNoteItem = this.AllocationNote.Items.FirstOrDefault(i => item.AllocationNoteItem != null && i.Id == item.AllocationNoteItem.Id);
                        if (allocationNoteItem != null)
                            allocationNoteItem.AuditInStockCount += item.Num;

                    }
                }
                else
                {
                    PurchaseRequisitionItem tempRequisitionItem;
                    foreach (var item in this.Items)
                    {
                        tempRequisitionItem = this.PurchaseRequisition.Items.FirstOrDefault(i => i.Id == item.PurchaseRequisitionItem_Id);
                        if (tempRequisitionItem != null)
                            tempRequisitionItem.AuditPurchaseCount += item.OrdinaryStockCount + item.BulkProductStockCount;
                    }
                }

                this.AuditState = PurchaseInStockNoteAuditState.待财务审核;
                this.HandlingCost = handlingCost;
                this.LogisticsFee = logisticsFee;
            }
            else//更改入库单状态为拒绝
                this.AuditState = PurchaseInStockNoteAuditState.采购拒绝;


            //处理审核日志
            UserRoleInfo userRoleInfo = UserRoleInfo.GetUserRoleInfo(userRole);
            if (this.AuditTraces == null)
                this.AuditTraces = new List<PurchaseInStockNoteAuditTrace>();
            this.AuditTraces.Add(new PurchaseInStockNoteAuditTrace
            {
                AuditTime = DateTime.Now,
                MobileNo = userMobileNo,
                PurchaseInStockNote = this,
                UserId = userId,
                TraceState = passed ? AuditTraceState.审核通过 : AuditTraceState.审核拒绝,
                Remark = remark,
                UserName = userTrueName,
                UserRoleInfo = userRoleInfo,
                SequenceNo = userRoleInfo.Level,
            });
        }
        */
        #endregion

    }
}
