using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.PrizeNoteModule;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using ERP.Domain.Model.ProductStockModule;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Model.PrizeNoteModule
{
    /// <summary>
    /// 兑入奖券
    /// </summary>
    public class RaffleInTicket : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 兑入单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 兑入单据状态
        /// </summary>
        public RaffleInTicketState State { get; set; }

        /// <summary>
        /// 兑入单据 审核状态
        /// </summary>
        public RaffleInTicketAuditState AuditState { get; set; }

        /// <summary>
        /// 奖券编号
        /// </summary>
        [MaxLength(64)]
        public string TicketNo { get; set; }

        /// <summary>
        /// 兑入数量
        /// </summary>
        public int InCount { get; set; }

        /// <summary>
        /// 原兑入价格
        /// </summary>
        public decimal InPrice { get; set; }

        /// <summary>
        /// 实际兑入价格
        /// </summary>
        public decimal ActualInPrice { get; set; }

        /// <summary>
        /// 兑入金额
        /// </summary>
        public decimal InAmount { get; set; }

        [MaxLength(256)]
        public string Remark { get; set; }

        /// <summary>
        /// 关联的订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 是否特殊产品
        /// </summary>
        public bool IsParticularProduct { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("RaffleOutTicket")]
        public string RaffleOutTicket_Id { get; set; }
        /// <summary>
        /// 关联的兑出单
        /// </summary>
        public RaffleOutTicket RaffleOutTicket { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("ActualProduct")]
        public string ActualProduct_Id { get; set; }
        /// <summary>
        /// 实际兑入产品（审核后，库存或应收单均已该产品为准）
        /// </summary>
        public Product ActualProduct { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("ReceivableBill")]
        public string ReceivableBill_Id { get; set; }
        /// <summary>
        /// 只有特殊商品 审核通过后 才有关联。
        /// </summary>
        public ReceivableBill ReceivableBill { get; set; }

        #endregion

        #region 业务方法
        /*
        /// <summary>
        /// 审核(调用方 需判断ReceivableBill是否为null，不为null，需生成BillNo
        /// </summary>
        /// <param name="context"></param>
        public virtual void Audit(ERPContext context, RaffleInTicketAuditState auditState)
        {
            if (this.AuditState != RaffleInTicketAuditState.待审核)
                throw new BusinessException("只有待审核状态可进行审核操作！");

            if (auditState == RaffleInTicketAuditState.已审核)
            {
                if (this.ActualProduct.Id == Product.PrizeProductId || this.IsParticularProduct)
                {
                    ParticularProductAccounting(context);
                }
                else
                {
                    Accounting(context);
                }

                this.State = RaffleInTicketState.已兑入;
                this.AuditState = RaffleInTicketAuditState.已审核;
            }
            else
            {
                //回加兑奖单信息  待兑入数量
                if (this.RaffleOutTicket == null)
                {
                    throw new BusinessException("关联兑出单信息未加载，拒绝操作失败！");
                }
                this.RaffleOutTicket.InCount -= this.InCount;
                this.RaffleOutTicket.UpdateState();

                var raffleTicketInfo = context.RaffleTicketInfoes.FirstOrDefault(i => i.Product.Id == this.Product.Id && i.StoreHouse.Id == this.StoreHouse.Id);
                if (raffleTicketInfo == null)
                    throw new BusinessException("数据异常！");

                raffleTicketInfo.RaffleInAmount -= this.InAmount;
                raffleTicketInfo.RaffleInCount -= this.InCount;
                raffleTicketInfo.RaffleReceivableAmount += this.InAmount;

                this.State = RaffleInTicketState.未兑入;
                this.AuditState = RaffleInTicketAuditState.已拒绝;
            }

        }

        /// <summary>
        /// 特殊产品审核
        /// </summary>
        /// <param name="context"></param>
        private void ParticularProductAccounting(ERPContext context)
        {
            this.ReceivableBill = new ReceivableBill()
             {
                 CannotEditAndDelete = true,
                 AdValoremAmount = this.InAmount,
                 AuditState = Enums.CommonModule.AuditState.待审核,
                 ReceivableBillType = ReceivableBillType.应收单,
                 PaidInAmount = this.InAmount,
                 OverdueTime = DateTime.Now.AddMonths(1),
                 BusinessTime = DateTime.Now,
                 City = this.City,
                 IsInternal = true,
                 CompanyUserCity = this.City,
                 Items = new List<ReceivableBillItem>()
                {
                    new ReceivableBillItem()
                    {
                         AdValoremAmount = this.InAmount,
                         Price = this.ActualInPrice,
                         IncludeTaxPrice =this.ActualInPrice,
                         Num= this.InCount,
                         TaxAmount=0,
                         Unit = this.ActualProduct.Info.Desc.PackageUnit,
                         Product= this.ActualProduct,
                    },
                },
             };
        }

        private void Accounting(ERPContext context)
        {
            //处理库存 以及成本
            var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info")
                .FirstOrDefault(s => s.Product.Id == this.ActualProduct.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
            {
                productStock = new ProductStock()
                {
                    City_Id = this.City.Id,
                    City = this.City,
                    StoreHouse = this.StoreHouse,
                    Product = this.ActualProduct,
                    StockUnit = this.ActualProduct.Info.Desc.PackageUnit,
                    PriceUnit = this.ActualProduct.Info.Desc.PackageUnit,
                    CostPrice = this.InPrice,
                    StockNum = 0,
                };
                context.ProductStocks.Add(productStock);
            }

            productStock.AddStock(this.InCount, this.ActualInPrice);

            //核算
            DateTime now = DateTime.Now;
            FinancialStock endingBalance = FinancialStock.GetEndingBalance(this.ActualProduct, now.Year, now.Month, this.StoreHouse, this.City, context);

            FinancialStock stock = new FinancialStock()
            {
                Year = now.Year,
                Month = now.Month,
                BusinessId = this.NoteNo,
                BusinessItemId = this.Id,
                BusinessTime = now,
                StoreHouse = this.StoreHouse,
                City = this.City,
                Product = this.ActualProduct,
                Type = FinancialStockType.兑奖入库,
                IncomeNum = this.InCount,
                IncomePrice = this.ActualInPrice,
                IncomeAmount = this.InCount * this.ActualInPrice,
            };

            stock.Accounting(context, endingBalance, productStock);

            context.FinancialStocks.Add(stock);
            ProductLotManager lotManager = new ProductLotManager(context);
            lotManager.AddProductLot(this.StoreHouse, this.ActualProduct, this.InCount);
        }
        */
        #endregion
    }
}
