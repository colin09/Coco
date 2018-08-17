using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.StockBillModule
{
    [Table("BaseNotes")]
    public class BaseNote : BaseEntity, IDeleted, ICannotEditAndDelete
    {
        public BaseNote()
        {
            this.Unit = "瓶";
            this.AuditState = NoteAuditState.待审核;
        }

        #region 基本属性
        [MaxLength(64)]
        public string NoteNo { get; set; }
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public NoteType NoteType { get; set; }

        /// <summary>
        /// 单据操作类型
        /// </summary>
        public NoteOperationType OperationType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Num { get; set; }

        /// <summary>
        /// 单位(瓶)
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

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
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否不可编辑删除
        /// </summary>
        public bool CannotEditAndDelete { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }

        [ForeignKey("BaseNoteInfo")]
        [MaxLength(64)]
        public string BaseNoteInfo_Id { get; set; }
        public BaseNoteInfo BaseNoteInfo { get; set; }
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

            ProductLotManager lotManager = new ProductLotManager(context);

            //处理库存 以及成本
            var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info").FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock != null)
            {
                if (this.NoteType == NoteType.出库单)
                    productStock.AddStock(this.Num, this.Price);
                else
                    productStock.ReduceStock(this.Num, this.Price);
            }

            if (this.AuditState == NoteAuditState.已核算)
            {
                var financialStock = context.FinancialStocks.Include("Product").Include("StoreHouse")
                    .FirstOrDefault(s => s.BusinessItemId == this.Id && s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                if (financialStock != null)
                    financialStock.UnAudit(context);
            }

            this.AuditState = NoteAuditState.待审核;
            this.AuditRemark = "";

            if (this.OperationType != NoteOperationType.库存盘点)
            {
                if (this.NoteType == NoteType.出库单)
                    lotManager.AddProductLot(this.StoreHouse, this.Product, this.Num);
                else
                    lotManager.ReduceProductLot(this.StoreHouse, this.Product, this.Num);
            }
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
                var productStock = HandleProductStock(context);

                //核算
                DateTime now = DateTime.Now;
                FinancialStock stock = new FinancialStock()
                {
                    Year = now.Year,
                    Month = now.Month,
                    BusinessId = this.NoteNo,
                    BusinessItemId = this.Id,
                    BusinessTime = now,
                    StoreHouse = this.StoreHouse,
                    City = this.StoreHouse.City,
                    Product = this.Product,
                    Type = this.OperationType.ToFinancialStockType(this.NoteType),
                };

                FinancialStock endingBalance = FinancialStock.GetEndingBalance(this.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);

                HandleFinancialStockData(endingBalance, stock);

                stock.Accounting(context, endingBalance, productStock);

                context.FinancialStocks.Add(stock);

                this.AuditState = NoteAuditState.已核算;
                if (this.OperationType != NoteOperationType.库存盘点)
                {
                    if (this.NoteType == NoteType.出库单)
                        lotManager.ReduceProductLot(this.StoreHouse, this.Product, this.Num);
                    else
                        lotManager.AddProductLot(this.StoreHouse, this.Product, this.Num);
                }

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
                //库存处理
                var productStock = HandleProductStock(context);

                //核算
                HandleCrossMonthAccounting(context, productStock);

                this.AuditState = NoteAuditState.已核算;

                if (this.OperationType != NoteOperationType.库存盘点)
                {
                    if (this.NoteType == NoteType.出库单)
                        lotManager.ReduceProductLot(this.StoreHouse, this.Product, this.Num);
                    else
                        lotManager.AddProductLot(this.StoreHouse, this.Product, this.Num);
                }
            }
        }

        private ProductStock HandleProductStock(ERPContext context)
        {
            var productStock = context.ProductStocks.Include("StoreHouse.City").Include("Product.Info")
            .FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
            {
                if (this.NoteType == NoteType.出库单)
                    throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", this.StoreHouse.Name, this.Product.Info.Desc.ProductName));
                else
                {
                    productStock = new ProductStock()
                    {
                        City_Id = this.StoreHouse.City.Id,
                        City = this.StoreHouse.City,
                        StoreHouse = this.StoreHouse,
                        Product = this.Product,
                        StockUnit = this.Unit,
                        PriceUnit = this.Unit,
                        CostPrice = this.Price,
                        StockNum = 0,
                    };
                    context.ProductStocks.Add(productStock);
                }
            }
            else
            {
                if (this.NoteType == NoteType.出库单)
                    productStock.ReduceStock(this.Num, this.Price);
                else
                    productStock.AddStock(this.Num, this.Price);
            }
            return productStock;
        }

        private void HandleCrossMonthAccounting(ERPContext context, ProductStock productStock)
        {
            DateTime accountingTime = this.BusinessTime;//结算时间

            var lastFinancialStock = FinancialStock.GetCrossMonthLastBalance(this.Product, accountingTime.Year, accountingTime.Month
              , this.StoreHouse, this.StoreHouse.City, context);

            var followUpFinancialStocks = FinancialStock.GetFollowUpFinancialStocks(lastFinancialStock.BusinessTime, this.Product, this.StoreHouse, this.StoreHouse.City, context);

            var financialStock = new FinancialStock()
            {
                Type = this.OperationType.ToFinancialStockType(this.NoteType),
                City = this.StoreHouse.City,
                Year = accountingTime.Year,
                Month = accountingTime.Month,
                BusinessId = this.NoteNo,
                BusinessItemId = this.Id,
                BusinessTime = lastFinancialStock.BusinessTime.AddSeconds(1),
                StoreHouse = this.StoreHouse,
                Product = this.Product,
            };

            HandleFinancialStockData(lastFinancialStock, financialStock);

            if (financialStock.BusinessTime > lastFinancialStock.GetEndingBusinessTime())
            {
                financialStock.BusinessTime = lastFinancialStock.BusinessTime;
                financialStock.Sequence = lastFinancialStock.Sequence + 1;
            }

            financialStock.CrossMonthAccounting(context, lastFinancialStock, followUpFinancialStocks, productStock);
            context.FinancialStocks.Add(financialStock);
        }

        private void HandleFinancialStockData(FinancialStock endingBalance, FinancialStock financialStock)
        {
            if (this.NoteType == NoteType.出库单)
            {
                financialStock.SendoutNum = this.Num;
                financialStock.SendoutPrice = endingBalance.BalancePrice;
                financialStock.SendoutAmount = this.Num * endingBalance.BalancePrice;
                this.Price = endingBalance.BalancePrice;
            }
            else
            {
                financialStock.IncomeNum = this.Num;
                if (this.Price == decimal.Zero)
                {
                    financialStock.IncomePrice = endingBalance.BalancePrice;
                    financialStock.IncomeAmount = this.Num * endingBalance.BalancePrice;
                    this.Price = endingBalance.BalancePrice;
                }
                else
                {
                    financialStock.IncomePrice = this.Price;
                    financialStock.IncomeAmount = this.Num * this.Price;
                }
            }
        }
        public void CheckEdit()
        {
            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可编辑！");
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");
        }
        public void CheckDelete()
        {
            if (this.IsDeleted) return;
            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可删除！");
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");
        }
        public virtual void Delete()
        {
            if (this.IsDeleted) return;
            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可删除！");
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            this.IsDeleted = true;
        }
        */
        #endregion
    }
}
