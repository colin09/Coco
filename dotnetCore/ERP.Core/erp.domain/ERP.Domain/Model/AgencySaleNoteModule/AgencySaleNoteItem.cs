using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgencySaleNoteModule
{
    public class AgencySaleNoteItem : BaseEntity
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Note")]
        public string Note_Id { get; set; }
        /// <summary>
        /// 所属代销售单据
        /// </summary>
        public AgencySaleNote Note { get; set; }
        [MaxLength(64)]
        [ForeignKey("FromProduct")]
        public string FromProduct_Id { get; set; }
        /// <summary>
        /// 总部产品
        /// </summary>
        public Product FromProduct { get; set; }
        [MaxLength(64)]
        [ForeignKey("ToProduct")]
        public string ToProduct_Id { get; set; }
        /// <summary>
        /// 对应区域的产品
        /// </summary>
        public Product ToProduct { get; set; }

        #endregion
    }
}
