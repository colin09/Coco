using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductLotModule
{
    /// <summary>
    /// 产品批次
    /// </summary>
    public class ProductLot : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 采购单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseInStockNote_Id { get; set; }
        /// <summary>
        /// 采购单项编号
        /// </summary>
        [MaxLength(64)]
        public string InStockItem_Id { get; set; }

        public int Version { get; set; }

        public int Num { get; set; }
        [MaxLength(64)]
        public string Unit { get; set; }
        [MaxLength(64)]
        public string Specifications { get; set; }
        public int PackageQuantity { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        [MaxLength(64)]
        [ForeignKey("Provider")]
        public string Provider_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        #region 聚合属性

        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        #endregion
    }
}
