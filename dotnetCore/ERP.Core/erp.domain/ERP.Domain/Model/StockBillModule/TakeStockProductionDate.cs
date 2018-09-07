using ERP.Domain.Model.ProductLotModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    [Table("TakeStockProductionDate")]
    public class TakeStockProductionDate : BaseEntity
    {
        #region 基础信息
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 扩展信息（供应链使用）
        /// </summary>
        public string ExtendInfo { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("TakeStockItem")]
        public string TakeStockItem_Id { get; set; }
        public TakeStockItem TakeStockItem { get; set; }
        [MaxLength(64)]
        [ForeignKey("ProductLot")]
        public string ProductLot_Id { get; set; }
        /// <summary>
        /// 产品批次库存
        /// </summary>
        public ProductLot ProductLot { get; set; }

        #endregion
    }
}
