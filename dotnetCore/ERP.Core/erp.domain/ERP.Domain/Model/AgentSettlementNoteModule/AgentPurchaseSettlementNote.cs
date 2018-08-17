using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseInStockModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentPurchaseSettlementNote : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 采购
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 已结算数量
        /// </summary>
        public int SettlementCount { get; set; }

        /// <summary>
        /// 入库价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 入库裸价
        /// </summary>
        public decimal BarePrice { get; set; }

        #region 冗余字段
        /// <summary>
        /// 采购入库单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseNoteNo { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(16)]
        public string Unit { get; set; }
        [MaxLength(16)]
        public string Specifications { get; set; }
        public int PackageQuantity { get; set; }

        #endregion

        #endregion

        #region 聚合属性

        /// <summary>
        /// 总部入库单
        /// </summary>
        [ForeignKey("PurchaseInStockNote")]
        [MaxLength(64)]
        public string PurchaseInStockNote_Id { get; set; }
        public PurchaseInStockNote PurchaseInStockNote { get; set; }

        [ForeignKey("AgentCity")]
        [MaxLength(64)]
        public string AgentCity_Id { get; set; }

        /// <summary>
        /// 代理城市（易酒批代理）
        /// </summary>
        public City AgentCity { get; set; }

        [ForeignKey("AgentStoreHouse")]
        [MaxLength(64)]
        public string AgentStoreHouse_Id { get; set; }

        /// <summary>
        /// 代理仓库（易酒批代理）
        /// </summary>
        public StoreHouse AgentStoreHouse { get; set; }

        [ForeignKey("SettlementCity")]
        [MaxLength(64)]
        public string SettlementCity_Id { get; set; }
        /// <summary>
        /// 结算城市
        /// </summary>
        public City SettlementCity { get; set; }

        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        public Product Product { get; set; }

        #endregion

        internal static void ModelConfiguration(ModelBuilder modelBuilder)
        {
            #region   保证财务的精度
            var properties = new[]
            {
                modelBuilder.Entity<AgentPurchaseSettlementNote>().Property(s => s.Price),
                modelBuilder.Entity<AgentPurchaseSettlementNote>().Property(s => s.BarePrice),
            };

            properties.ToList().ForEach(property =>
            {
                //property.HasPrecision(38, 6);
            });
            #endregion
        }
    }
}
