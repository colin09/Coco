using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialStockModule
{
    /// <summary>
    /// 财务库存
    /// </summary>
    public class FinancialStock : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 单据类型
        /// </summary>
        public FinancialStockType Type { get; set; }

        /// <summary>
        /// 会计结算年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 会计结算月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string BusinessId { get; set; }

        /// <summary>
        /// 单据项编号
        /// </summary>
        [MaxLength(64)]
        public string BusinessItemId { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 排序号（结算时间相同时，根据Sequence判断先后关系）
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 收入数量
        /// </summary>
        public int IncomeNum { get; set; }
        /// <summary>
        /// 收入单价
        /// </summary>
        public decimal IncomePrice { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal IncomeAmount { get; set; }
        /// <summary>
        /// 发出数量
        /// </summary>
        public int SendoutNum { get; set; }
        /// <summary>
        /// 发出单价
        /// </summary>
        public decimal SendoutPrice { get; set; }
        /// <summary>
        /// 发出金额
        /// </summary>
        public decimal SendoutAmount { get; set; }

        /// <summary>
        /// 结存数量
        /// </summary>
        public int BalanceNum { get; set; }

        /// <summary>
        /// 结存单价
        /// </summary>
        public decimal BalancePrice { get; set; }

        /// <summary>
        /// 结存金额
        /// </summary>
        public decimal BalanceAmount { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }

        #endregion

        #region 业务方法

        /// <summary>
        /// 获取该月 最大的BusinessTime（默认比期末结存少1秒）
        /// </summary>
        /// <returns></returns>
        public DateTime GetEndingBusinessTime()
        {
            return new DateTime(this.BusinessTime.Year, this.BusinessTime.Month, 1).AddMonths(1).AddSeconds(-2);
        }
        /*
        /// <summary>
        /// 会计核算（处理期末库存）
        /// </summary>
        public void Accounting(ERPContext context, FinancialStock endingBalance, ProductStock productStock = null)
        {
            //var endingBalance = GetEndingBalance(this.Product, this.Year, this.Month, this.StoreHouse, this.City, context);

            if (this.IncomeNum != 0 || this.IncomeAmount != Decimal.Zero)
            {
                endingBalance.BalanceNum += this.IncomeNum;
                endingBalance.BalanceAmount += this.IncomeAmount;
                endingBalance.BalancePrice = endingBalance.BalanceNum == 0 ? endingBalance.BalancePrice : endingBalance.BalanceAmount / endingBalance.BalanceNum;
            }
            else
            {
                endingBalance.BalanceNum -= this.SendoutNum;
                endingBalance.BalanceAmount -= this.SendoutAmount;
                endingBalance.BalancePrice = endingBalance.BalanceNum == 0 ? endingBalance.BalancePrice : endingBalance.BalanceAmount / endingBalance.BalanceNum;

                if (endingBalance.BalanceNum < 0)
                    throw new BusinessException(string.Format("产品：{0}库存不足，请先采购并成本核算后再进行后续操作！", endingBalance.Product.Id));
            }
            endingBalance.LastUpdateTime = DateTime.Now;
            this.BalanceNum = endingBalance.BalanceNum;
            this.BalancePrice = endingBalance.BalancePrice;
            this.BalanceAmount = endingBalance.BalanceAmount;

            //更新实时库存 成本价
            if (productStock == null)
                productStock = context.ProductStocks.First(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            productStock.CostPrice = endingBalance.BalancePrice;
            productStock.LastUpdateTime = DateTime.Now;
        }

        public void CrossMonthAccounting(ERPContext context, FinancialStock lastBalance, List<FinancialStock> followUpFinancialStocks, ProductStock productStock = null)
        {
            if (this.IncomeNum != 0 || this.IncomeAmount != Decimal.Zero)
            {
                followUpFinancialStocks.ForEach(s =>
                    {
                        s.BalanceNum += this.IncomeNum;
                        s.BalanceAmount += this.IncomeAmount;
                        s.BalancePrice = s.BalanceNum == 0 ? s.BalancePrice : s.BalanceAmount / s.BalanceNum;
                        s.LastUpdateTime = DateTime.Now;
                    });

                this.BalanceNum = lastBalance.BalanceNum + this.IncomeNum;
                this.BalanceAmount = lastBalance.BalanceAmount + this.IncomeAmount;
                this.BalancePrice = this.BalanceNum == 0 ? 0 : this.BalanceAmount / this.BalanceNum;
            }
            else
            {
                followUpFinancialStocks.ForEach(s =>
                {
                    s.BalanceNum -= this.SendoutNum;
                    s.BalanceAmount -= this.SendoutAmount;
                    s.BalancePrice = s.BalanceNum == 0 ? s.BalancePrice : s.BalanceAmount / s.BalanceNum;
                    s.LastUpdateTime = DateTime.Now;
                });

                this.BalanceNum = lastBalance.BalanceNum - this.SendoutNum;
                this.BalanceAmount = lastBalance.BalanceAmount - this.SendoutAmount;
                this.BalancePrice = this.BalanceNum == 0 ? 0 : this.BalanceAmount / this.BalanceNum;
            }

            var endingBalance = followUpFinancialStocks.Where(s => s.Type == FinancialStockType.期末结存).OrderByDescending(s => s.BusinessTime).FirstOrDefault();
            if (endingBalance.BalanceNum < 0)
                throw new BusinessException(string.Format("产品：{0}库存不足，请先采购并成本核算后再进行后续操作！", this.Product.Id));

            //更新实时库存 成本价
            if (productStock == null)
                productStock = context.ProductStocks.First(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            productStock.CostPrice = endingBalance.BalancePrice;
            productStock.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="context"></param>
        public void UnAudit(ERPContext context)
        {
            //晚于当前 结算时间的 记录，相关数量/单价/金额 修改（改操作 包括期末结存）
            var list = context.FinancialStocks.Where(s =>
                s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id
                && (s.BusinessTime > this.BusinessTime || (s.BusinessTime == this.BusinessTime && s.Sequence > this.Sequence))
                ).OrderByDescending(o => o.BusinessTime).ToList();

            if (this.IncomeNum != 0 || this.IncomeAmount != Decimal.Zero)
            {
                list.ForEach(s =>
                    {
                        s.BalanceNum -= this.IncomeNum;
                        s.BalanceAmount -= this.IncomeAmount;
                        s.BalancePrice = s.BalanceNum == 0 ? s.BalancePrice : s.BalanceAmount / s.BalanceNum;
                    });
            }
            else
            {
                list.ForEach(s =>
                {
                    s.BalanceNum += this.SendoutNum;
                    s.BalanceAmount += this.SendoutAmount;
                    s.BalancePrice = s.BalanceNum == 0 ? s.BalancePrice : s.BalanceAmount / s.BalanceNum;
                });
            }

            var endingBalance = list.FirstOrDefault(s => s.Type == FinancialStockType.期末结存);

            //更新实时库存 成本价
            var productStock = context.ProductStocks.First(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
            productStock.CostPrice = endingBalance.BalancePrice;

            context.FinancialStocks.Remove(this);
        }

        /// <summary>
        /// 获取期初财务库存
        /// </summary>
        /// <param name="product"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="storeHouse"></param>
        /// <param name="city"></param>
        /// <param name="context"></param>
        /// <param name="addEntity">是否添加实体到数据库</param>
        /// <returns></returns>
        private static FinancialStock GetOpeningBalance(Product product, int year, int month, StoreHouse storeHouse, City city, ERPContext context, bool addEntity = true)
        {
            var result = context.FinancialStocks.FirstOrDefault(s => s.Product.Id == product.Id && s.Year == year && s.Month == month
                && s.StoreHouse.Id == storeHouse.Id && s.Type == FinancialStockType.期初结存);

            if (result == null)
            {
                //期初结存
                result = new FinancialStock()
                    {
                        Type = FinancialStockType.期初结存,

                        BusinessTime = new DateTime(year, month, 1),
                        Year = year,
                        Month = month,
                        StoreHouse = storeHouse,
                        City = city,
                        Product = product,
                    };

                //DateTime prevMonth = new DateTime(year, month, 1).AddMonths(-1);
                //int tempYear = prevMonth.Year, tempMonth = prevMonth.Month;
                var prevMonthBalance = context.FinancialStocks.Where(s =>
                    s.Type == FinancialStockType.期末结存
                    && s.Product.Id == product.Id
                    && s.StoreHouse.Id == storeHouse.Id
                    && (s.Year < year || (s.Year == year && s.Month < month))
                    //   && s.Year == tempYear && s.Month == tempMonth
                    ).OrderByDescending(s => s.BusinessTime).FirstOrDefault();

                if (prevMonthBalance != null)
                {
                    result.BalancePrice = prevMonthBalance.BalancePrice;
                    result.BalanceAmount = prevMonthBalance.BalanceAmount;
                    result.BalanceNum = prevMonthBalance.BalanceNum;
                    if (result.BalanceNum != 0 && addEntity)
                        context.FinancialStocks.Add(result); //只有上月有 期末库存时，才加入 期初库存
                }
            }

            return result;
        }

        private static decimal GetLastPurchasePrice(Product product, StoreHouse storeHouse, ERPContext context)
        {
            if (product == null || storeHouse == null)
                return decimal.Zero;
            //最后一条 采购审核记录。
            var lastPurchaseRecord = context.FinancialStocks.AsNoTracking().Where(s => s.Product.Id == product.Id
                && s.StoreHouse.Id == storeHouse.Id && s.Type == FinancialStockType.采购入库单 && s.IncomeAmount != decimal.Zero)
                .OrderByDescending(s => s.BusinessTime).ThenByDescending(s => s.Sequence).FirstOrDefault();
            if (lastPurchaseRecord != null)
                return lastPurchaseRecord.IncomePrice;

            //没有采购的 取最近一次期初（结存单价大于0的）
            var openingBalance = context.FinancialStocks.AsNoTracking().Where(s => s.Product.Id == product.Id && s.StoreHouse.Id == storeHouse.Id
                && s.Type == FinancialStockType.期初结存 && s.BalancePrice > 0).OrderByDescending(s => s.BusinessTime).FirstOrDefault();

            if (openingBalance != null) return openingBalance.BalancePrice;

            return decimal.Zero;
        }

        /// <summary>
        /// 获取期末财务库存
        /// </summary>
        /// <param name="product"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="storeHouse"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static FinancialStock GetEndingBalance(Product product, int year, int month, StoreHouse storeHouse, City city, ERPContext context)
        {
            var result = context.FinancialStocks.Include("Product").Include("City").Include("StoreHouse")
                .FirstOrDefault(s => s.Product.Id == product.Id && s.Year == year && s.Month == month
                && s.StoreHouse.Id == storeHouse.Id && s.Type == FinancialStockType.期末结存);

            if (result == null)
            {
                //期末结存
                result = new FinancialStock()
                {
                    Type = FinancialStockType.期末结存,

                    BusinessTime = new DateTime(year, month, 1).AddMonths(1).AddSeconds(-1),
                    Year = year,
                    Month = month,
                    StoreHouse = storeHouse,
                    City = city,
                    Product = product,
                };

                context.FinancialStocks.Add(result);

                var openingBalance = GetOpeningBalance(product, year, month, storeHouse, city, context);
                if (openingBalance != null)
                {
                    result.BalancePrice = openingBalance.BalancePrice;
                    result.BalanceAmount = openingBalance.BalanceAmount;
                    result.BalanceNum = openingBalance.BalanceNum;
                }
            }

            if (result.BalanceNum == 0 || result.BalancePrice == decimal.Zero)
                result.BalancePrice = GetLastPurchasePrice(product, storeHouse, context);

            return result;
        }

        public static FinancialStock GetCrossMonthLastBalance(Product product, int year, int month, StoreHouse storeHouse, City city, ERPContext context)
        {
            //当月最后一条结存记录
            var balance = context.FinancialStocks.Include("Product").Include("StoreHouse").AsNoTracking()
                .OrderByDescending(s => s.BusinessTime).ThenByDescending(s => s.Sequence).FirstOrDefault(s =>
                s.Product.Id == product.Id && s.Year == year && s.Month == month
               && s.StoreHouse.Id == storeHouse.Id &&
               s.Type != FinancialStockType.期末结存);

            if (balance == null)
            {
                //本月无数据时，生成一条期初库存
                balance = GetOpeningBalance(product, year, month, storeHouse, city, context, false);
            }

            return balance;
        }

        /// <summary>
        /// 获取跨月结算时间 之后发生的所有存货收发记录
        /// </summary>
        /// <param name="time"></param>
        /// <param name="product"></param>
        /// <param name="storeHouse"></param>
        /// <param name="city"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<FinancialStock> GetFollowUpFinancialStocks(DateTime time, Product product, StoreHouse storeHouse, City city, ERPContext context)
        {
            var financialStocks = context.FinancialStocks.Where(s => s.BusinessTime > time && s.Product.Id == product.Id
                && s.StoreHouse.Id == storeHouse.Id && s.City.Id == city.Id).ToList();
            //结算月无期末结存的，添加结存
            if (financialStocks.Count(s => s.Year == time.Year && s.Month == time.Month && s.Type == FinancialStockType.期末结存) == 0)
            {
                financialStocks.Add(GetEndingBalance(product, time.Year, time.Month, storeHouse, city, context));
            }

            //结算月期末结存
            var currentEndingBalance = financialStocks.FirstOrDefault(s => s.Type == FinancialStockType.期末结存 && s.Year == time.Year && s.Month == time.Month);
            //结算次月
            DateTime nextMonth = time.AddMonths(1);

            //结算日期次月无存货收发明细，跨月核算后 应有期初和期末
            if (financialStocks.Count(s => s.Year == nextMonth.Year && s.Month == nextMonth.Month) == 0)
            {
                var nextMonthOpeningBalance = new FinancialStock()
                {
                    Type = FinancialStockType.期初结存,
                    Product = product,
                    StoreHouse = storeHouse,
                    City = city,
                    BusinessTime = new DateTime(nextMonth.Year, nextMonth.Month, 1),
                    Year = nextMonth.Year,
                    Month = nextMonth.Month,
                    BalanceNum = currentEndingBalance.BalanceNum,
                    BalanceAmount = currentEndingBalance.BalanceAmount,
                    BalancePrice = currentEndingBalance.BalancePrice,
                };

                var nextMonthEndingBalance = new FinancialStock()
                {
                    Type = FinancialStockType.期末结存,
                    Product = product,
                    StoreHouse = storeHouse,
                    City = city,
                    BusinessTime = new DateTime(nextMonth.Year, nextMonth.Month, 1).AddMonths(1).AddSeconds(-1),
                    Year = nextMonth.Year,
                    Month = nextMonth.Month,
                    BalanceNum = currentEndingBalance.BalanceNum,
                    BalanceAmount = currentEndingBalance.BalanceAmount,
                    BalancePrice = currentEndingBalance.BalancePrice,
                };
                context.FinancialStocks.Add(nextMonthOpeningBalance);
                context.FinancialStocks.Add(nextMonthEndingBalance);
                financialStocks.Add(nextMonthOpeningBalance);
                financialStocks.Add(nextMonthEndingBalance);

            }

            //存在 次月存货明细并且无期初结存，跨月后 应有期初库存,需添加
            if (financialStocks.Count(s => s.Year == nextMonth.Year && s.Month == nextMonth.Month) > 0
                && financialStocks.Count(s => s.Year == nextMonth.Year && s.Month == nextMonth.Month && s.Type == FinancialStockType.期初结存) == 0)
            {
                var openingBalance = new FinancialStock()
                {
                    Type = FinancialStockType.期初结存,
                    Product = product,
                    StoreHouse = storeHouse,
                    City = city,
                    BusinessTime = new DateTime(nextMonth.Year, nextMonth.Month, 1),
                    Year = nextMonth.Year,
                    Month = nextMonth.Month,
                };

                //如果结算月存在期末结存，则下月期初 取期末结存数据。
                if (currentEndingBalance != null)
                {
                    openingBalance.BalanceNum = currentEndingBalance.BalanceNum;
                    openingBalance.BalancePrice = currentEndingBalance.BalancePrice;
                    openingBalance.BalanceAmount = currentEndingBalance.BalanceAmount;
                }

                financialStocks.Add(openingBalance);
                context.FinancialStocks.Add(openingBalance);
            }

            return financialStocks;
        }
        */
        #endregion
    }
}
