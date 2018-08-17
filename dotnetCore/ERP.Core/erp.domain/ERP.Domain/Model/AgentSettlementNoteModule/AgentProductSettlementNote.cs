using ERP.Domain.Enums.AgentSettlementNoteModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema; // for Annotations Index
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AgentSettlementNoteModule
{
    public class AgentProductSettlementNote : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 结算单号
        /// </summary>
        [MaxLength(64)]
        //[Index(IsClustered = false)]
        [Index]
        public string NoteNo { get; set; }

        /// <summary>
        /// 结算状态
        /// </summary>
        public AgentProductSettlementNoteState State { get; set; }

        /// <summary>
        /// 结算数量
        /// </summary>
        public int SettlementCount { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleCount { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public int PurchaseCount { get; set; }

        /// <summary>
        /// 入库价
        /// </summary>
        public decimal PurchasePrice { get; set; }
        /// <summary>
        /// 入库裸价
        /// </summary>
        public decimal PurchaseBarePrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 销售结算金额
        /// </summary>
        public decimal SettlementSaleAmount { get; set; }

        /// <summary>
        ///  返利结算金额
        /// </summary>
        public decimal SettlementRebateAmount { get; set; }

        /// <summary>
        /// 结算总额
        /// </summary>
        [NotMapped]
        public decimal SettlementAmount
        {
            get
            {
                return this.SettlementSaleAmount + this.SettlementRebateAmount;
            }
        }

        /// <summary>
        /// 区域支付时间
        /// </summary>
        public DateTime? SettlementCityPayTime { get; set; }

        /// <summary>
        /// 总部确认时间
        /// </summary>
        public DateTime? AgentCityComfirmTime { get; set; }

        #region 冗余字段
        /// <summary>
        /// 采购入库单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseNoteNo { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }
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

        /// <summary>
        /// 区域同步到总部
        /// </summary>
        [ForeignKey("OrderOutStockNote")]
        [MaxLength(64)]
        public string OrderOutStockNote_Id { get; set; }
        public OrderOutStockNote OrderOutStockNote { get; set; }

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

        #region 业务方法

        public void ComputeSettlementAmount()
        {
            this.SettlementSaleAmount = this.SettlementCount * this.SalePrice;
            this.SettlementRebateAmount = (this.PurchasePrice - this.PurchaseBarePrice) * this.SettlementCount;
        }

        #endregion

        internal static void ModelConfiguration(ModelBuilder modelBuilder)
        {
            #region   保证财务的精度
            var properties = new[]
            {
                modelBuilder.Entity<AgentProductSettlementNote>().Property(s => s.PurchasePrice),
                modelBuilder.Entity<AgentProductSettlementNote>().Property(s => s.PurchaseBarePrice),
                modelBuilder.Entity<AgentProductSettlementNote>().Property(s => s.SalePrice),
                modelBuilder.Entity<AgentProductSettlementNote>().Property(s => s.SettlementSaleAmount),
                modelBuilder.Entity<AgentProductSettlementNote>().Property(s => s.SettlementRebateAmount),
            };

            properties.ToList().ForEach(property =>
            {
                //property.HasPrecision(38, 6);
            });
            #endregion
        }
    }
}
