using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.PrizeNoteModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.PrizeNoteModule
{
    public class PrizeNote : BaseEntity, IAggregationRoot
    {
        public PrizeNote()
        {
            this.State = PrizeNoteState.已兑出;
        }

        #region 基本属性

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(64)]
        public string Title { get; set; }

        public int Num { get; set; }

        public decimal Price { get; set; }

        [MaxLength(32)]
        public string Unit { get; set; }

        public PrizeNoteState State { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }

        #endregion

        #region 业务方法
        /*
        /// <summary>
        /// 兑出
        /// </summary>
        /// <param name="context"></param>
        public virtual void Out(ERPContext context)
        {
            //处理库存 以及成本
            var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info")
                .FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
                throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", this.StoreHouse.Name, this.Product.Info.Desc.ProductName));

            productStock.ReduceStock(this.Num, this.Price);

            ProductLotManager lotManager = new ProductLotManager(context);
            //核算
            DateTime now = DateTime.Now;
            FinancialStock endingBalance = FinancialStock.GetEndingBalance(this.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);
            this.Price = endingBalance.BalancePrice;

            productStock.PrizeNoteAdd(this.Num, this.Price);

            FinancialStock stock = new FinancialStock()
            {
                Year = now.Year,
                Month = now.Month,
                BusinessId = this.Id,
                BusinessItemId = this.Id,
                BusinessTime = now,
                StoreHouse = this.StoreHouse,
                City = this.StoreHouse.City,
                Product = this.Product,
                Type = FinancialStockType.兑奖出库,
                SendoutNum = this.Num,
                SendoutPrice = endingBalance.BalancePrice,
                SendoutAmount = this.Num * endingBalance.BalancePrice,
            };

            stock.Accounting(context, endingBalance, productStock);

            context.FinancialStocks.Add(stock);
            lotManager.ReduceProductLot(this.StoreHouse, this.Product, this.Num);
            this.State = PrizeNoteState.已兑出;
        }

        /// <summary>
        /// 兑入
        /// </summary>
        /// <param name="context"></param>
        public virtual void In(ERPContext context)
        {
            if (this.State != PrizeNoteState.已兑出)
                throw new BusinessException("该单据不可兑入");

            //处理库存 以及成本
            var productStock = context.ProductStocks
                .Include("StoreHouse").Include("Product.Info")
                .FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock == null)
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

            productStock.AddStock(this.Num, this.Price);

            productStock.PrizeNoteReduce(this.Num, this.Price);

            //核算
            DateTime now = DateTime.Now;
            FinancialStock endingBalance = FinancialStock.GetEndingBalance(this.Product, now.Year, now.Month, this.StoreHouse, this.StoreHouse.City, context);

            FinancialStock stock = new FinancialStock()
            {
                Year = now.Year,
                Month = now.Month,
                BusinessId = this.Id,
                BusinessItemId = this.Id,
                BusinessTime = now,
                StoreHouse = this.StoreHouse,
                City = this.StoreHouse.City,
                Product = this.Product,
                Type = FinancialStockType.兑奖入库,
                IncomeNum = this.Num,
                IncomePrice = this.Price,
                IncomeAmount = this.Num * this.Price,
            };

            stock.Accounting(context, endingBalance, productStock);

            context.FinancialStocks.Add(stock);
            ProductLotManager lotManager = new ProductLotManager(context);
            lotManager.AddProductLot(this.StoreHouse, this.Product, this.Num);
            this.State = PrizeNoteState.已兑入;
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="context"></param>
        public virtual void Cancel(ERPContext context)
        {
            if (this.State != PrizeNoteState.已兑出)
                throw new BusinessException("该单据不可作废");

            if (this.ExpiredTime > DateTime.Now)
                throw new BusinessException("单据尚未过期，不可作废");

            //处理库存 以及成本
            var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info").FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            if (productStock != null)
            {
                productStock.PrizeNoteReduce(this.Num, this.Price);
            }

            this.State = PrizeNoteState.作废;
        }
        */
        #endregion
    }
}
