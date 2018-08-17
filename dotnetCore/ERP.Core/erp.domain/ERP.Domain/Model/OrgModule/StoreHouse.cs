using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.ShopModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    [Table("StoreHouse")]
    public class StoreHouse : Org, IAggregationRoot
    {
        public StoreHouse()
            : base()
        {
            this.OrgType = OrgModule.OrgType.仓库;
            this.StoreHouseType = StoreHouseType.城市仓库;
        }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public StoreHouseType StoreHouseType { get; set; }

        public bool IsSupplierStore { get; set; }

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("Shop")]
        public string Shop_Id { get; set; }
        #region 聚合关系

        public City City { get; set; }

        public Shop Shop { get; set; }

        #endregion
    }
}
