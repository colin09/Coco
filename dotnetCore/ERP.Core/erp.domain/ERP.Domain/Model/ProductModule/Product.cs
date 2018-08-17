using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.ProductModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule.Desc;
using ERP.Domain.Model.ShopModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ProductModule
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("Product")]
    public class Product : BaseEntity, IAggregationRoot, INewId
    {
        public const string FinancialOpeningProductId = "9999999999999999999";
        public const string FinancialOpeningProductName = "财务期初数据";
        public const string PrizeProductId = "9999999999999999998";
        public const string PrizeProductName = "特殊商品";
        public static readonly Product FinancialOpeningProduct = new Product()
        {
            Id = FinancialOpeningProductId,
            ProductType = ProductType.自营产品,
            State = ProductState.下架,
            Info = new ProductInfo()
            {
                Id = FinancialOpeningProductId,
                Desc = new ProductDesc()
                {
                    ProductName = FinancialOpeningProductName,
                    ProductNo = FinancialOpeningProductId,
                },
            },
        };
        /// <summary>
        /// 兑奖特殊产品
        /// </summary>
        public static readonly Product PrizeProduct = new Product()
        {
            Id = PrizeProductId,
            ProductType = ProductType.自营产品,
            State = ProductState.下架,
            Info = new ProductInfo()
            {
                Id = PrizeProductId,
                Desc = new ProductDesc()
                {
                    ProductName = PrizeProductName,
                    ProductNo = FinancialOpeningProductId,
                    PackageQuantity = 1,
                    Specifications = "个",
                    PackageUnit = "个"
                },
            },
        };


        #region 基本属性

        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        public ProductState State { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? OnSaleTime { get; set; }

        /// <summary>
        /// 各区域自定义的品名
        /// </summary>
        [MaxLength(256)]
        public string ProductName { get; set; }

        [MaxLength(64)]
        public string NewId { get; set; }

        public bool IsAreaAgent { get; set; }

        #endregion

        #region 聚合关系

        [MaxLength(64)]
        [ForeignKey("Info")]
        public string Info_Id { get; set; }

        /// <summary>
        /// 产品的容器
        /// </summary>
        public virtual ProductInfo Info { get; set; }

        [MaxLength(64)]
        [ForeignKey("Shop")]
        public string Shop_Id { get; set; }

        /// <summary>
        /// 只有入驻模式有值
        /// </summary>
        public Shop Shop { get; set; }

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public City City { get; set; }
  
        #endregion

        #region 公共方法
        /*
        public static Product GetFinancialOpeningProduct(ERPContext context)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == FinancialOpeningProductId);
            if (product == null)
            {
                product = FinancialOpeningProduct;
                context.Products.Add(product);
            }

            return product;
        }
        */
        #endregion
    }
}
