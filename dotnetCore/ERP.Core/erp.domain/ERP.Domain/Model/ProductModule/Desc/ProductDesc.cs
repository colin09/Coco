using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.ProductModule;
using ERP.Domain.Model.CommonModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule.Desc
{
    /// <summary>
    /// 产品描述
    /// </summary>    
    public class ProductDesc : IValueObject
    {
        #region 基本属性

        ///<summary>
        ///产品编号
        ///</summary>
        [MaxLength(50)]
        public string ProductNo { get; set; }


        ///<summary>
        ///品牌名称
        ///</summary>
        [MaxLength(50)]
        public string BrandName { get; set; }

        ///<summary>
        ///产品名称
        ///</summary>
        [MaxLength(100)]
        public string ProductName { get; set; }

        ///<summary>
        ///规格(大单位)
        ///</summary>
        [MaxLength(500)]
        public string Specifications { get; set; }

        /// <summary>
        /// 是否统采独家
        /// </summary>
        public OwnProductType OwnProductType { get; set; }

        /// <summary>
        /// 包装数量
        /// </summary>
        public int PackageQuantity { get; set; }

        /// <summary>
        /// 包装单位(小单位)
        /// </summary>
        [MaxLength(32)]
        public string PackageUnit { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [MaxLength(100)]
        public string Category { get; set; }

        /// <summary>
        /// 旧版一级类目
        /// </summary>
        [MaxLength(100)]
        public string OldCategory { get; set; }

        /// <summary>
        /// 商品子分类
        /// </summary>
        [MaxLength(100)]
        public string SubCategory { get; set; }

        #endregion
    }
}
