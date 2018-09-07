using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    [Table("TakeStockItem")]
    public class TakeStockItem : BaseEntity
    {
        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        public int TakeNum { get; set; }

        /// <summary>
        /// 盘点结果
        /// </summary>
        public int TakeResultNum { get; set; }

        /// <summary>
        /// 盈亏金额
        /// </summary>
        public decimal ProfitLossAmount { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }
                
        /// <summary>
        /// 盘点次数
        /// </summary>
        public int TakeCount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("Note")]
        public string Note_Id { get; set; }
        public TakeStockNote Note { get; set; }

        public List<TakeStockProductionDate> TakeStockProductionDate { get; set; }
        #endregion

        #region 方法
        public int GetTakeResourNumLarge(int quantity=0) {
            if (quantity == 0) {
                if (Product != null && Product.Info != null) {
                    return TakeResultNum / Product.Info.Desc.PackageQuantity;
                }
                return 0;
            }
            return TakeResultNum / quantity;
        }
        public int GetTakeResourNumSmall(int quantity = 0)
        {
            if (quantity == 0)
            {
                if (Product != null && Product.Info != null)
                {
                    return TakeResultNum % Product.Info.Desc.PackageQuantity;
                }
                return TakeResultNum;
            }
            return TakeResultNum % quantity;
        }
        #endregion
    }
}
