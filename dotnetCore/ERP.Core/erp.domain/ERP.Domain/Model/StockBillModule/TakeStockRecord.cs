using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    /// <summary>
    /// 盘点记录（每周动盘产品已被盘点数）
    /// </summary>
    [Table("TakeStockRecord")]
    public class TakeStockRecord : BaseEntity
    {
        #region 基础信息

        /// <summary>
        /// 每周动盘产品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 已盘产品数量
        /// </summary>
        public int TakeCount { get; set; }

        /// <summary>
        /// 动盘产品是否全部盘点
        /// </summary>
        public bool IsTake { get; set; }

        /// <summary>
        /// 已生成盘点单产品数量
        /// </summary>
        public int TakeNoteCount { get; set; }

        /// <summary>
        /// 动盘产品是否全部生成盘点单
        /// </summary>
        public bool IsTakeNote { get; set; }
        #endregion

        #region 聚合属性

        public List<TakeStockNote> TakeStockNotes { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }

        #endregion

        #region 方法
        public void SetIsTake()
        {
            if (this.TakeCount == this.Count)
                this.IsTake = true;
            else
                this.IsTake = false;
        }

        public void SetIsTakeNote()
        {
            if (this.TakeNoteCount == this.Count)
                this.IsTakeNote = true;
            else
                this.IsTakeNote = false;
        }
        #endregion
    }
}
