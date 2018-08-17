using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    /// <summary>
    /// 采购申请单 检查数据（针对某一采购申请单项）
    /// </summary>
    public class PurchaseRequisitionCheckData : IValueObject
    {
        /// <summary>
        /// 是否新产品
        /// </summary>
        public bool IsNewProduct { get; set; }

        /// <summary>
        /// 区域平均采购单价（区域平均采购单价需提前计算）
        /// </summary>
        public decimal RegionAveragePurchasePrice { get; set; }

        /// <summary>
        /// 平台售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 上次采购单价
        /// </summary>
        public decimal LastPurchasePrice { get; set; }

        /// <summary>
        /// 最近**天最高采购价
        /// </summary>
        public decimal MaxPurchasePrice { get; set; }

        /// <summary>
        /// 本地库存数量
        /// </summary>
        public int StoreCount { get; set; }
        /// <summary>
        /// 近15天销量
        /// </summary>
        public int SaleCount_15 { get; set; }
        /// <summary>
        /// 近30天销量
        /// </summary>
        public int SaleCount_30 { get; set; }
        /// <summary>
        /// 近60天销量
        /// </summary>
        public int SaleCount_60 { get; set; }
    }
}
