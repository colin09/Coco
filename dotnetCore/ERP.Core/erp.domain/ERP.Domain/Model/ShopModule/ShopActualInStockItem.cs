using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    public class ShopActualInStockItem : BaseEntity
    {
        /// <summary>
        /// 入库编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 货位
        /// </summary>
        [MaxLength(64)]
        public string GoodsPosition { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [MaxLength(50)]
        public string BatchNo { get; set; }

        [MaxLength(64)]
        [ForeignKey("ShopInStockItem")]
        public string ShopInStockItem_Id { get; set; }

        /// <summary>
        /// 关联的经销商入库单项
        /// </summary>
        public ShopInStockItem ShopInStockItem { get; set; }
    }
}
