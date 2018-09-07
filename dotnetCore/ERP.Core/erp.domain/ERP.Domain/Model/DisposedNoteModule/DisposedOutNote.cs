using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.DisposedNoteModule
{
    /// <summary>
    /// 处理品转出记录
    /// </summary>
    public class DisposedOutNote : BaseEntity
    {
        /// <summary>
        /// 转出数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 转出时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 关联的单据编号
        /// </summary>
        [MaxLength(64)]
        public string BusinessNoteNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 转出产品
        /// </summary>
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion
    }
}
