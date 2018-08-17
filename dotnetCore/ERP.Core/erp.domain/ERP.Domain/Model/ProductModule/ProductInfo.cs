using ERP.Domain.Enums.ProductModule;
using ERP.Domain.Model.ProductModule.Desc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    /// <summary>
    /// 产品信息
    /// </summary>
    [Table("ProductInfo")]
    public class ProductInfo : BaseEntity, IAggregationRoot, INewId
    {
        #region 基本信息

        /// <summary>
        /// 酒类非酒类
        /// </summary>
        public ProductBusinessClass ProductBusinessClass { get; set; }

        /// <summary>
        /// 产品信息状态
        /// </summary>
        public ProductInfoState ProductInfoState { get; set; }

        /// <summary>
        /// 2.0 产品信息Id （2.0产品信息对应多个规格，到ERP这边 会生成多个产品信息）
        /// </summary>
        [MaxLength(64)]
        public string NewVersionInfoId { get; set; }

        [MaxLength(64)]
        public string NewId { get; set; }

        /// <summary>
        /// 配送系数
        /// </summary>
        public double DistributionPercent { get; set; }

        /// <summary>
        /// 保质期（天）
        /// </summary>
        public int MonthOfShelfLife { get; set; }

        #endregion

        #region 聚合关系

        /// <summary>
        /// 产品基本信息
        /// </summary>
        public ProductDesc Desc { get; set; }

        #endregion
    }
}
