using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.ProductStockModule
{
    /// <summary>
    /// 产品库存
    /// </summary>
    /// <remarks>
    /// ERP库存与OP库存分开，手动同步
    /// </remarks>
    public class ProductStock : BaseEntity
    {
        public ProductStock()
        {
            this.StockUnit = "瓶";
            this.PriceUnit = "瓶";
        }

        #region 基本属性

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockNum { get; set; }

        /// <summary>
        /// 处理品出库数量
        /// </summary>
        public int DisposedOutCount { get; set; }

        /// <summary>
        /// 代销售出库数量
        /// </summary>
        public int AgencySaleCount { get; set; }

        /// <summary>
        /// 库存单位(瓶)
        /// </summary>
        [MaxLength(32)]
        public string StockUnit { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }

        /// <summary>
        /// 价格单位(瓶)
        /// </summary>
        [MaxLength(32)]
        public string PriceUnit { get; set; }
       

        #region 奖券相关

        public int PrizeNoteCount { get; set; }
        public int PrizeNoteProductNum { get; set; }
        public decimal PrizeNoteAmount { get; set; }

        #endregion

        #endregion

        #region 聚合属性
        /// <summary>
        /// 城市Id
        /// </summary>
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// 入库更新库存
        /// </summary>
        /// <param name="Num">入库数量</param>
        /// <param name="Price">入库价格</param>
        public void AddStock(int Num, decimal Price)
        {
            this.StockNum += Num;
        }

        /// <summary>
        /// 出库更新库存
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="Price"></param>
        public void ReduceStock(int Num, decimal Price)
        {
            int tempNum = this.StockNum - Num;
            if (tempNum < 0)
            {
                if (this.StoreHouse != null && this.Product != null && this.Product.Info != null)
                    throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", this.StoreHouse.Name, this.Product.Info.Desc.ProductName));
                else
                    throw new BusinessException("库存不足！");
            }
            this.StockNum -= Num;
        }

        public void PrizeNoteAdd(int num, decimal price)
        {
            this.PrizeNoteCount++;
            this.PrizeNoteProductNum += num;

            this.PrizeNoteAmount += num * price;
        }

        public void PrizeNoteReduce(int num, decimal price)
        {
            this.PrizeNoteCount--;
            this.PrizeNoteProductNum -= num;

            this.PrizeNoteAmount -= num * price;

            if (PrizeNoteCount <= 0 || this.PrizeNoteProductNum <= 0)
            {
                this.PrizeNoteCount = 0;
                this.PrizeNoteProductNum = 0;
                this.PrizeNoteAmount = 0;
            }
        }

        public void AgencySaleAdd(int num)
        {
            this.StockNum -= num;
            this.AgencySaleCount += num;
        }

        public void AgencySaleReduce(int num)
        {
            this.StockNum += num;
            this.AgencySaleCount -= num;
        }

        /// <summary>
        /// 处理品转出（加当前库存，减处理品数量）
        /// </summary>
        /// <param name="num"></param>
        public void DisposedOut(int num)
        {
            this.StockNum += num;
            this.DisposedOutCount -= num;
        }
        /// <summary>
        /// 处理品转入（减当前库存，加处理品数量）
        /// </summary>
        /// <param name="num"></param>
        public void DisposedIn(int num)
        {
            this.StockNum -= num;
            if (this.StockNum < 0) throw new BusinessException("转入处理品数量不能大于当前库存数量！");
            this.DisposedOutCount += num;
        }

        #endregion
    }
}
