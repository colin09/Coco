using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class CompetingProductItem : BaseEntity
    {
        /// <summary>
        /// 竞品类型
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string CompetingProductType { get; set; }

        /// <summary>
        /// 平台销售价（扣除立减）
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 竞品价格
        /// </summary>
        public decimal CompetingProductPrice { get; set; }
        /// <summary>
        /// 竞品底价（可为空）
        /// </summary>
        public decimal? CompetingProductFloorPrice { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 竞品 有无此商品
        /// </summary>
        public bool NoProduct { get; set; }

        #region 聚合属性

        /// <summary>
        /// 采购申请单项
        /// </summary>
        public PurchaseRequisitionItem PurchaseRequisitionItem { get; set; }

        public City City { get; set; }

        public Product Product { get; set; }

        #endregion
    }
}
