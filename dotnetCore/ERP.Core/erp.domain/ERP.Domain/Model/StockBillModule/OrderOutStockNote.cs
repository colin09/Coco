using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.SettlementNoteItemModule;
using ERP.Domain.Model.UserModule;
using ERP.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.StockBillModule
{
    /// <summary>
    /// 销售出库单
    /// </summary>
    [Table("OrderOutStockNote")]
    public class OrderOutStockNote : BaseEntity, IAggregationRoot, IDeleted, IHidden, ICannotEditAndDelete, INotSyncStock, IHasCreateRelatedNote
    {
        public OrderOutStockNote()
        {
            this.AuditState = NoteAuditState.待审核;
            this.IsDownReason = false;
            this.IsDeleted = false;
            this.IsInternal = false;
        }

        #region 基本属性

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 销售类型
        /// </summary>
        public SaleBusinessType SaleBusinessType { get; set; }

        /// <summary>
        /// 大货批发模式
        /// </summary>
        public BigGoodsMode BigGoodsMode { get; set; }

        /// <summary>
        /// 出库日期
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
        /// 是否来源于代销售单据
        /// </summary>
        public bool IsFromAgencySaleNote { get; set; }
        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal OddAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 现金标记金额
        /// </summary>
        public decimal CashMarkAmount { get; set; }

        /// <summary>
        /// 银行付款标记金额
        /// </summary>
        public decimal BankMarkAmount { get; set; }

        /// <summary>
        /// 微信标记金额
        /// </summary>
        public decimal WeiXinMarkAmount { get; set; }
        /// <summary>
        /// 支付宝标记金额
        /// </summary>
        public decimal AlipayMarkAmount { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 兑奖红包金额
        /// </summary>
        public decimal UseRewardBonusAmount { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否不能编辑、删除
        /// </summary>
        public bool CannotEditAndDelete { get; set; }
        /// <summary>
        /// 是否不同步库存
        /// </summary>
        public bool IsNotSyncStock { get; set; }
        /// <summary>
        /// 是否已创建关联单据
        /// </summary>
        public bool HasCreateRelatedNote { get; set; }
        /// <summary>
        /// 是否隐藏数据
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 是否内部单据
        /// </summary>
        public bool IsInternal { get; set; }

        [MaxLength(64)]
        public string NoteNO { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [MaxLength(50)]
        public string Operators { get; set; }

        #region 冗余字段

        /// <summary>
        /// 订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 调拨单编号
        /// </summary>
        [MaxLength(64)]
        public string AllocationNoteNo { get; set; }

        #endregion

        #endregion

        #region 聚合属性


        [ForeignKey("AllocationNote")]
        [MaxLength(64)]
        public string AllocationNote_Id { get; set; }

        /// <summary>
        /// 关联的调拨单
        /// </summary>
        public AllocationNote AllocationNote { get; set; }

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
        [ForeignKey("User")]
        [MaxLength(64)]
        public string User_Id { get; set; }
        public User User { get; set; }

        [ForeignKey("CompanyUserCity")]
        [MaxLength(64)]
        public string CompanyUserCity_Id { get; set; }
        /// <summary>
        /// 只有内部往来 才不为NULL
        /// </summary>
        public Org CompanyUserCity { get; set; }

        [ForeignKey("BrokerUser")]
        [MaxLength(64)]
        public string BrokerUser_Id { get; set; }
        public OrgUser BrokerUser { get; set; }

        public List<NoteItem> Items { get; set; }

        #endregion

        #region 方法

        public void EditCheck()
        {
            if (this.IsFromAgencySaleNote)
                throw new BusinessException("代销售单据产生的出库单，不可编辑！");
            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可编辑！");
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("当前状态不允许编辑！");
            if (!string.IsNullOrWhiteSpace(this.Order_Id))
                throw new BusinessException("订单下推的销售出库单不允许编辑！");
        }

        public void DeleteCheck()
        {
            if (this.CannotEditAndDelete) throw new BusinessException("该单据不可删除！");
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");
        }
        
        /*
        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="context"></param>
        public virtual void UnAudit(ERPContext context)
        {
            if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
                throw new BusinessException("当前状态不能反审核");

            if (context.ReceivableBills.WhereNotDeleted().Any(b => b.OrderOutStockNote.Id == this.Id))
                throw new BusinessException(string.Format("单据{0}不能反审核,请先删除应收单！", this.Id));

            HandleSettlementUnAuditLogic(context);

            //记录反审核的 收发存Id，避免单据出现重复产品 出现的各种问题
            List<string> financialStockIds = new List<string>();

            ProductLotManager lotManager = new ProductLotManager(context);

            foreach (var item in Items)
            {
                //处理库存 以及成本
                var productStock = context.ProductStocks.FirstOrDefault(s => s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                if (productStock != null)
                {
                    productStock.AddStock(item.Num, item.Price);
                }

                if (this.AuditState == NoteAuditState.已核算)
                {
                    var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
                        .FirstOrDefault(s => (s.BusinessItemId == item.Id || s.BusinessItemId == this.Id) && s.Product.Id == item.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id
                        && s.SendoutNum == item.Num && !financialStockIds.Contains(s.Id));
                    if (financialStock != null)
                    {
                        financialStockIds.Add(financialStock.Id);
                        financialStock.UnAudit(context);
                    }
                }
                lotManager.AddProductLot(this.StoreHouse, item.Product, item.Num);
            }

            this.AuditState = NoteAuditState.待审核;
            this.AuditRemark = "";

            if (this.Order != null)
            {
                //对应的退货入库单
                var returnOrderInStockNote = context.ReturnOrderInStockNotes
                    .Include("Items.Product").Include("StoreHouse").FirstOrDefault(n => n.Order.Id == this.Order.Id);
                if (returnOrderInStockNote != null)
                {
                    if (returnOrderInStockNote.AuditState == NoteAuditState.审核通过 || returnOrderInStockNote.AuditState == NoteAuditState.已核算)
                        throw new BusinessException(string.Format("请先反审核订单编号为{0}的退货入库单！", this.Order.OrderNo));
                }
            }
        }

        private void HandleSettlementUnAuditLogic(ERPContext context)
        {
            if (this.Items.Where(i => !string.IsNullOrEmpty(i.SettlementNoteItem_Id)).Count() == 0)
                return;
            string[] settlementNoteItemIds = this.Items.Where(i => !string.IsNullOrEmpty(i.SettlementNoteItem_Id)).Select(i => i.SettlementNoteItem_Id).ToArray();
            List<SettlementNoteItem> settlementNoteItems = context.SettlementNoteItems.Where(i => settlementNoteItemIds.Contains(i.Id)).ToList();
            if (settlementNoteItems.Any(i => i.HasSettlement))
            {
                throw new BusinessException("出库单项结算单明细已核算，不可反审核单据！");
            }

            settlementNoteItems.ForEach(i => i.IsHidden = true);
        }

        public virtual void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;

            if (auditState == NoteAuditState.审核通过)
            {
                StringBuilder message = new StringBuilder();
                ProductStock productStock;

                DateTime now = DateTime.Now;
                List<FinancialStock> financialStockList = new List<FinancialStock>();
                List<ProductStock> productStockList = new List<ProductStock>();
                FinancialStock financialStock;
                FinancialStock endingBalance;
                ProductLotManager lotManager = new ProductLotManager(context);
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

                    HandleProductStock(context, productStockList, item, message, out productStock);

                    if (message.Length == 0)
                    {
                        endingBalance = financialStockList.FirstOrDefault(s => s.Product.Id == item.Product.Id
                                       && s.Year == now.Year && s.Month == now.Month
                                       && s.StoreHouse.Id == this.StoreHouse.Id && s.City.Id == this.StoreHouse.City.Id);
                        if (endingBalance == null)
                        {
                            endingBalance = FinancialStock.GetEndingBalance(item.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);
                            financialStockList.Add(endingBalance);
                        }

                        financialStock = new FinancialStock()
                        {
                            Year = now.Year,
                            Month = now.Month,
                            Type = FinancialStockType.销售出库单,
                            BusinessId = this.NoteNO,
                            BusinessItemId = item.Id,
                            BusinessTime = now,
                            Sequence = sequenceDic[key],
                            StoreHouse = this.StoreHouse,
                            City = this.StoreHouse.City,
                            Product = item.Product,
                            SendoutNum = item.Num,
                            SendoutPrice = endingBalance.BalancePrice,
                            SendoutAmount = item.Num * endingBalance.BalancePrice,
                        };

                        item.CostPrice = endingBalance.BalancePrice;
                        item.LastUpdateTime = DateTime.Now;

                        financialStock.Accounting(context, endingBalance);
                        context.FinancialStocks.Add(financialStock);

                        lotManager.ReduceProductLot(this.StoreHouse, item.Product, item.Num);
                    }
                }

                if (message.Length > 0)
                    throw new BusinessException(message.ToString());

                this.AuditState = NoteAuditState.已核算;
            }
        }

        public virtual void CrossMonthAuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        {
            if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
            this.AuditRemark = auditRemark;
            StringBuilder message = new StringBuilder();
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
                    // 库存处理
                    HandleProductStock(context, productStockList, item, message, out productStock);

                    //存货收发
                    HandleCrossMonthAccounting(item, followUpFinancialStocksDic, lastFinancialStockList, productStock, context);
                    lotManager.ReduceProductLot(this.StoreHouse, item.Product, item.Num);
                }
                if (message.Length > 0) throw new BusinessException(message.ToString());
                this.AuditState = NoteAuditState.已核算;
            }
        }

        private void HandleProductStock(ERPContext context, List<ProductStock> productStockList, NoteItem item, StringBuilder message, out ProductStock productStock)
        {
            productStock = productStockList.FirstOrDefault(p => p.Product.Id == item.Product.Id && p.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
            {
                //同步库存
                productStock = context.ProductStocks.Include("Product").Include("StoreHouse").FirstOrDefault(p =>
                    p.Product.Id == item.Product.Id && p.StoreHouse.Id == this.StoreHouse.Id);
                productStockList.Add(productStock);
            }
            if (productStock == null)
                throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", this.StoreHouse.Name, item.Product.Info.Desc.ProductName));

            if (!item.IsGiveaway)
            {
                var costPrice = Math.Round(productStock.CostPrice, 6, MidpointRounding.AwayFromZero);
                //手动新增的销售出库单金额校验（不能小于当前成本价）
                if (this.Order == null && !this.IsInternal && item.Price < costPrice)
                {
                    message.AppendFormat("产品：{0}销售价{1}不能小于成本价{2}；", item.Product.Info.Desc.ProductName, item.Price, costPrice);
                }
            }
            try
            {
                productStock.ReduceStock(item.Num, item.Price);
            }
            catch (BusinessException ex)
            {
                message.AppendFormat(ex.Message);
            }
        }

        private void HandleCrossMonthAccounting(NoteItem item, Dictionary<string, List<FinancialStock>> followUpFinancialStocksDic, List<FinancialStock> lastFinancialStockList, ProductStock productStock, ERPContext context)
        {
            var accountingTime = this.BusinessTime;

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
            List<FinancialStock> followUpFinancialStocks;
            followUpFinancialStocksDic.TryGetValue(key, out followUpFinancialStocks);
            if (followUpFinancialStocks == null || followUpFinancialStocks.Count == 0)
            {
                followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime, item.Product, this.StoreHouse, this.StoreHouse.City, context);
                followUpFinancialStocksDic.Add(key, followUpFinancialStocks);
            }

            var financialStock = new FinancialStock()
             {
                 Type = FinancialStockType.销售出库单,
                 City = this.StoreHouse.City,
                 Year = accountingTime.Year,
                 Month = accountingTime.Month,
                 BusinessId = this.NoteNO,
                 BusinessItemId = item.Id,
                 BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
                 StoreHouse = this.StoreHouse,
                 Product = item.Product,
                 SendoutNum = item.Num,
                 SendoutPrice = lastFinancialStock.BalancePrice,
                 SendoutAmount = item.Num * lastFinancialStock.BalancePrice,
             };

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            item.CostPrice = lastFinancialStock.BalancePrice;
            item.LastUpdateTime = DateTime.Now;
            financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks, productStock);
            context.FinancialStocks.Add(financialStock);
            lastFinancialStockList.Add(financialStock);
        }

        /// <summary>
        /// 新增或更新出库单项结算单
        /// </summary>
        /// <param name="note"></param>
        /// <param name="item"></param>
        /// <param name="context"></param>
        private void AddOrUpdateSettlementNoteItem(NoteItem item, ERPContext context)
        {
            SettlementNoteItem settlementNoteItem;
            if (string.IsNullOrWhiteSpace(item.SettlementNoteItem_Id))
            {
                settlementNoteItem = new SettlementNoteItem()
                {
                    NoteNO = this.NoteNO,
                    City = this.City,
                    StoreHouse = this.StoreHouse,
                    SaleBusinessType = this.SaleBusinessType,
                    Product = item.Product,
                };
                item.SettlementNoteItem = settlementNoteItem;
            }
            else
            {
                settlementNoteItem = context.SettlementNoteItems.FirstOrDefault(i => i.Id == item.SettlementNoteItem_Id);
            }
            settlementNoteItem.BusinessTime = this.BusinessTime;
            settlementNoteItem.Num = item.Num;
            settlementNoteItem.Price = item.Price;
            settlementNoteItem.AdValoremAmount = item.AdValoremAmount;
            settlementNoteItem.IsHidden = false;
        }

        public virtual void Delete(ERPContext context)
        {
            if (this.IsDeleted)
                return;

            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可删除！");

            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            if (this.Order != null)
            {
                if (context.ReceivableBills.WhereNotDeleted().Any(b => b.OrderNo == this.Order.OrderNo && b.AdValoremAmount >= 0))
                    throw new BusinessException("请先删除应收单！");

                this.Order.HasCreateNote = false;
            }

            HandleSettlementDeleteLogic(context);

            this.IsDeleted = true;
        }

        /// <summary>
        /// 处理 总部代理结算删除逻辑
        /// </summary>
        /// <param name="context"></param>
        private void HandleSettlementDeleteLogic(ERPContext context)
        {
            string[] settlementNoteItemIds = this.Items.Where(i => !string.IsNullOrEmpty(i.SettlementNoteItem_Id))
                .Select(i => i.SettlementNoteItem_Id).ToArray();
            if (settlementNoteItemIds.Length == 0) return;

            SettlementNoteItem[] settlementNoteItems = context.SettlementNoteItems.Where(i => settlementNoteItemIds.Contains(i.Id)).ToArray();
            if (settlementNoteItems.Any(i => i.HasSettlement))
            {
                throw new BusinessException("销售明细结算单已核算，不可删除！");
            }

            context.SettlementNoteItems.RemoveRange(settlementNoteItems);

        }

        */
        #endregion

    }
}
