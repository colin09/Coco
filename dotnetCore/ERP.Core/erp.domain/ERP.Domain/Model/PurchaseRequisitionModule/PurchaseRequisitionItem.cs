using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.PurchaseRequisitionModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.ProductModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class PurchaseRequisitionItem : BaseEntity
    {
        public PurchaseRequisitionItem()
            : base()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.CompetingProductItems = new List<CompetingProductItem>();
            this.AuditState = CommonAuditState.待审核;
        }

        #region 基本属性
        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 建议零售价
        /// </summary>
        public decimal AdviceRetailPrice { get; set; }

        /// <summary>
        /// 供应商确认价格（大单位）
        /// </summary>
        public decimal ProviderConfirmPrice { get; set; }
        /// <summary>
        /// 申请单项审核状态
        /// </summary>
        public CommonAuditState AuditState { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(64)]
        public string Unit { get; set; }

        /// <summary>
        /// 采购申请数量
        /// </summary>
        public int PurchaseRequisitionCount { get; set; }

        /// <summary>
        /// 供应商确认数量
        /// </summary>
        public int ProviderConfirmCount { get; set; }

        /// <summary>
        /// 采购入库数量（已录入采购入库单的数量，录入采购入库单时需更新）
        /// 调拨数量（添加调拨单时需更新）
        /// </summary>
        public int PurchaseCount { get; set; }

        /// <summary>
        /// 采购已审核的采购数量（采购入库单采购已审核的数量，采购审核时 需更新）
        /// </summary>
        public int AuditPurchaseCount { get; set; }
        /// <summary>
        /// 大货库存数量
        /// </summary>
        public int BulkProductStockCount { get; set; }
        /// <summary>
        /// 普通库存数量
        /// </summary>
        public int OrdinaryStockCount { get; set; }

        /// <summary>
        /// 采购总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 销售范围
        /// </summary>
        public SaleType SaleType { get; set; }

        /// <summary>
        /// 销售城市（只有销售范围是 部分城市的才需要输入）
        /// </summary>
        [MaxLength(256)]
        public string SaleCities { get; set; }

        /// <summary>
        /// 是否返利
        /// </summary>
        public bool WhetherRebate { get; set; }

        /// <summary>
        /// 是否赠品
        /// </summary>
        public bool IsGiveaway { get; set; }
        /// <summary>
        /// 是否已叫货
        /// </summary>
        public bool HasCallGoods { get; set; }

        /// <summary>
        /// 返利类型
        /// </summary>
        public RebateType RebateType { get; set; }

        /// <summary>
        /// 入库裸价
        /// </summary>
        public decimal BarePrice { get; set; }

        /// <summary>
        /// 单件返利
        /// </summary>
        public decimal SingleRebate { get; set; }

        /// <summary>
        /// 货返政策
        /// </summary>
        [MaxLength(256)]
        public string RebatePolicy { get; set; }

        /// <summary>
        /// 返利总额
        /// </summary>
        public decimal RebateTotalAmount { get; set; }

        /// <summary>
        /// 设置RelatedProductIds的容器，只能对象赋值，不可Add、Remove操作
        /// </summary>
        [NotMapped]
        public List<string> RelatedProductIdContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RelatedProductIds))
                    return new List<string>(0);

                return RelatedProductIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            set
            {
                if (value == null)
                {
                    RelatedProductIds = string.Empty;
                }
                else
                {
                    RelatedProductIds = string.Join(";", value);
                }
            }
        }

        /// <summary>
        /// 赠品关联的产品Id（通过RelatedProductIdContainer 读取、设置）
        /// </summary>
        [MaxLength(1000)]
        public string RelatedProductIds { get; set; }

        /// <summary>
        /// 核查数据
        /// </summary>
        public PurchaseRequisitionCheckData CheckData { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 所属产品
        /// </summary>
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("PurchaseRequisition")]
        public string PurchaseRequisition_Id { get; set; }
        /// <summary>
        /// 所属采购申请单
        /// </summary>
        public PurchaseRequisition PurchaseRequisition { get; set; }

        /// <summary>
        /// 竞品信息（可能为空）
        /// </summary>
        public List<CompetingProductItem> CompetingProductItems { get; set; }

        #endregion
    }
}
