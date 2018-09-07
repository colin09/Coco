using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    public class ProductAllocationItem : BaseEntity
    {
        #region 基本属性

        [Display(Name = "数量")]
        public int Count { get; set; }

        [MaxLength(20)]
        [Display(Name = "单位")]
        public string Unit { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }

        [MaxLength(256)]
        [Display(Name = "备注")]
        public string Remark { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("ProductAllocation")]
        public string ProductAllocation_Id { get; set; }
        public ProductAllocation ProductAllocation { get; set; }

        #endregion
    }
}
