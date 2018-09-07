using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema; // for Annotations Index
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    /// <summary>
    /// 产品映射表（CityId  ProductId均为 2.0Id）在合并城市订单转换时 新增、更新数据。
    /// </summary>
    public class ProductMap : BaseEntity, IAggregationRoot
    {
        //[Index(IsClustered = false, IsUnique = false)]
        [Index]
        [MaxLength(64)]
        public string FromCityId { get; set; }

        //[Index(IsClustered = false, IsUnique = false)]
        [Index]
        [MaxLength(64)]
        public string FromProductId { get; set; }

        //[Index(IsClustered = false, IsUnique = false)]
        [Index]
        [MaxLength(64)]
        public string ToCityId { get; set; }

        //[Index(IsClustered = false, IsUnique = false)]
        [Index]
        [MaxLength(64)]
        public string ToProductId { get; set; }
    }
}
