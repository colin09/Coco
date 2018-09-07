using ERP.Domain.Model.ProductModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    public class ShopPickupNoteItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(64)]
        public string Unit { get; set; }

        /// <summary>
        /// 申请提货数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 已提货数量
        /// </summary>
        public int PickupCount { get; set; }

        #endregion

        #region 聚合属性

        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 所属产品
        /// </summary>
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("ShopPickupNote")]
        public string ShopPickupNote_Id { get; set; } 
        /// <summary>
        /// 所属提货单
        /// </summary>
        public ShopPickupNote ShopPickupNote { get; set; }

        #endregion
    }
}
