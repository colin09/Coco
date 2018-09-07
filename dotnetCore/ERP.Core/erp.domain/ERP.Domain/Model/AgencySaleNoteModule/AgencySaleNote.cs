using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.AgencySaleNoteModule;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Enums.PurchaseInStockModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.StockBillModule;
using ERP.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ERP.Domain.Enums.ProductModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.ProductStockModule;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Model.AgencySaleNoteModule
{
    /// <summary>
    /// 代销售单据
    /// </summary>
    public class AgencySaleNote : BaseEntity, IAggregationRoot
    {
        public AgencySaleNote()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }
        [MaxLength(64)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public AgencySaleNoteState State { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("FromCity")]
        public string FromCity_Id { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
        public City FromCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("FromStoreHouse")]
        public string FromStoreHouse_Id { get; set; }
        /// <summary>
        /// 出库仓库
        /// </summary>
        public StoreHouse FromStoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("ToCity")]
        public string ToCity_Id { get; set; }
        /// <summary>
        /// 分派城市
        /// </summary>
        public City ToCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("ToStoreHouse")]
        public string ToStoreHouse_Id { get; set; }
        /// <summary>
        /// 分派城市仓库
        /// </summary>
        public StoreHouse ToStoreHouse { get; set; }

        /// <summary>
        /// 代销售单据项
        /// </summary>
        public List<AgencySaleNoteItem> Items { get; set; }
        [MaxLength(64)]
        [ForeignKey("Order")]
        public string Order_Id { get; set; }
        /// <summary>
        /// 所属订单（总部）
        /// </summary>
        public Order Order { get; set; }
        [MaxLength(64)]
        [ForeignKey("ToOrderOutStockNote")]
        public string ToOrderOutStockNote_Id { get; set; }
        /// <summary>
        /// 区域销售出库单
        /// </summary>
        public OrderOutStockNote ToOrderOutStockNote { get; set; }
        [MaxLength(64)]
        [ForeignKey("PurchaseInStockNote")]
        public string PurchaseInStockNote_Id { get; set; }
        /// <summary>
        /// 总部采购入库单
        /// </summary>
        public PurchaseInStockNote PurchaseInStockNote { get; set; }
        #endregion

        #region 业务方法
        /* 
        /// <summary>
        /// 初始化代销售单据（生成代销售单据时，需要调用）
        /// </summary>
        /// <param name="context"></param>
        public virtual void Init(ERPContext context)
        {
            //确认后，会将 生成代销商出库单时扣除的库存列表库存 还原，并修改代销商出库数量，
            //并在区域生成一笔集团内部销售出库单（具有特殊标识，不可删除、不可修改，审核后不扣减平台库存）
            List<ProductStock> productStocks = new List<ProductStock>(this.Items.Count);
            ProductStock productStock;
            foreach (var item in this.Items)
            {
                productStock = productStocks.FirstOrDefault(s => s.Product.Id == item.ToProduct.Id);
                if (productStock == null)
                {
                    productStock = context.ProductStocks.Include("Product").FirstOrDefault(s => s.StoreHouse.Id == this.ToStoreHouse.Id && s.Product.Id == item.ToProduct.Id);
                    if (productStock == null)
                    {
                        productStock = new ProductStock()
                        {
                            City_Id = this.ToCity.Id,
                            City = this.ToCity,
                            StoreHouse = this.ToStoreHouse,
                            Product = item.ToProduct,
                            CostPrice = 0,
                            StockNum = 0,
                            StockUnit = item.ToProduct.Info.Desc.PackageUnit,
                            PriceUnit = item.ToProduct.Info.Desc.PackageUnit,
                        };
                        context.ProductStocks.Add(productStock);
                    }
                    productStocks.Add(productStock);
                }
                productStock.AgencySaleAdd(item.Num);
            }
        }

        /// <summary>
        /// 补货
        /// </summary>
        /// <param name="context"></param>
        public virtual void SendGoods()
        {
            if (this.State != AgencySaleNoteState.待确认)
                throw new BusinessException("当前状态不可补货！");
            this.State = AgencySaleNoteState.待确认收货;
        }
        /// <summary>
        /// 补钱
        /// </summary>
        /// <param name="context"></param>
        public virtual void SendMoney()
        {
            if (this.State != AgencySaleNoteState.待确认)
                throw new BusinessException("当前状态不可补货！");
            this.State = AgencySaleNoteState.待确认收款;

            this.PurchaseInStockNote = new PurchaseInStockNote()
            {
                IsFromAgencySaleNote = true,
                CannotEditAndDelete = true,
                IsNotSyncStock = true,
                City = this.FromCity,
                City_Id = this.FromCity_Id,
                SupplierCity = this.ToCity,
                SupplierCity_Id = this.ToCity_Id,
                StoreHouse = this.FromStoreHouse,
                StoreHouse_Id = this.FromStoreHouse_Id,
                IsInternal = true,
                InStockTime = DateTime.Now,
                AuditState = PurchaseInStockNoteAuditState.待财务审核,
                Version = 1,
                Remark = "代销售单据补钱自动生成采购入库单",
                IsDownReason = false,
                Items = this.Items.Select(i => new InStockItem()
                {
                    Num = i.Num,
                    Price = i.Price,
                    TotalAmount = i.TotalAmount,
                    Product = i.FromProduct,
                    Product_Id = i.FromProduct_Id,
                    Unit = i.Unit,
                    IsGiveaway = i.Price == decimal.Zero,
                }).ToList(),
            };
            this.PurchaseInStockNote_Id = this.PurchaseInStockNote.Id;
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="context"></param>
        public virtual void ConfirmReceiptGoods(ERPContext context)
        {
            if (this.State != AgencySaleNoteState.待确认收货)
                throw new BusinessException("当前状态不可补货！");

            List<ProductStock> productStocks = new List<ProductStock>(this.Items.Count);
            ProductStock productStock;
            foreach (var item in this.Items)
            {
                productStock = productStocks.FirstOrDefault(s => s.Product.Id == item.ToProduct.Id);
                if (productStock == null)
                {
                    productStock = context.ProductStocks.Include("Product").First(s => s.StoreHouse.Id == this.ToStoreHouse.Id && s.Product.Id == item.ToProduct.Id);
                    productStocks.Add(productStock);
                }

                productStock.AgencySaleReduce(item.Num);
            }

            this.State = AgencySaleNoteState.已确认收货;
        }
        /// <summary>
        /// 确认收款
        /// </summary>
        /// <param name="context"></param>
        public virtual void ConfirmReceiptMoney(ERPContext context)
        {
            if (this.State != AgencySaleNoteState.待确认收款)
                throw new BusinessException("当前状态不可补货！");

            //确认后，会将 生成代销商出库单时扣除的库存列表库存 还原，并修改代销商出库数量，
            //并在区域生成一笔集团内部销售出库单（具有特殊标识，不可删除、不可修改，审核后不扣减平台库存）
            List<ProductStock> productStocks = new List<ProductStock>(this.Items.Count);
            ProductStock productStock;
            foreach (var item in this.Items)
            {
                productStock = productStocks.FirstOrDefault(s => s.Product.Id == item.ToProduct.Id);
                if (productStock == null)
                {
                    productStock = context.ProductStocks.Include("Product").First(s => s.StoreHouse.Id == this.ToStoreHouse.Id && s.Product.Id == item.ToProduct.Id);
                    productStocks.Add(productStock);
                }
                productStock.AgencySaleReduce(item.Num);
            }

            var brokerUser = context.OrgUserAuths.Include("OrgUser").FirstOrDefault(u => u.OrgId == this.ToCity.Id
                && u.AuthType == OrgUserAuthType.SaleUser);
            this.ToOrderOutStockNote = new OrderOutStockNote()
            {
                StoreHouse = this.ToStoreHouse,
                City = this.ToCity,
                CompanyUserCity = this.FromCity,
                IsInternal = true,
                IsFromAgencySaleNote = true,
                CannotEditAndDelete = true,
                IsNotSyncStock = true,
                BusinessTime = DateTime.Now,
                TotalAmount = this.TotalAmount,
                OrderAmount = this.TotalAmount,
                AuditState = NoteAuditState.待审核,
                Remark = "代销售单据确认收款自动生成销售出库单",
                BrokerUser = brokerUser == null ? null : brokerUser.OrgUser,
                Items = this.Items.Select(i => new NoteItem()
                {
                    Product = i.ToProduct,
                    AdValoremAmount = i.TotalAmount,
                    Num = i.Num,
                    Price = i.Price,
                    IsGiveaway = i.Price == decimal.Zero,
                    Unit = i.Unit,
                    SaleMode = i.ToProduct.ProductType.ToSaleMode(),
                }).ToList(),
            };

            this.State = AgencySaleNoteState.已确认收款;
        }*/

        #endregion
    }
}
