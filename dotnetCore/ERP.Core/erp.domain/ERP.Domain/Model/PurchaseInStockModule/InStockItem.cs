using ERP.Domain.Enums.PurchaseRequisitionModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseInStockModule
{
    /// <summary>
    /// 入库明细
    /// </summary>
    public class InStockItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 价格（最终审核通过价格）
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 修改价格(采购申请录入的采购入库单，审核修改价格时，修改价格大于Price，则触发审核流程）
        /// </summary>
        public decimal EditPrice { get; set; }

        /// <summary>
        /// 采购申请价格
        /// </summary>
        public decimal RequisitionPrice { get; set; }

        /// <summary>
        /// 是否需要价格审核
        /// </summary>
        public bool NeedPriceAudit { get; set; }

        /// <summary>
        /// 修改总金额
        /// </summary>
        public decimal EditTotalAmount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Num { get; set; }
        /// <summary>
        /// 大货库存数量
        /// </summary>
        public int BulkProductStockCount { get; set; }
        /// <summary>
        /// 普通库存数量
        /// </summary>
        public int OrdinaryStockCount { get; set; }

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 单位(瓶)
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否是采购赠品
        /// </summary>
        public bool IsGiveaway { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        [MaxLength(32)]
        public string GoodsPosition { get; set; }

        /// <summary>
        /// 是否返利
        /// </summary>
        public bool WhetherRebate { get; set; }

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
        /// 生产日期
        /// </summary>
        public DateTime? ProductionDate { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// 返利类型
        /// </summary>
        public RebateType RebateType { get; set; }

        /// <summary>
        /// 入库裸价
        /// </summary>
        public decimal BarePrice { get; set; }

        /// <summary>
        /// 修改入库裸价
        /// </summary>
        public decimal EditBarePrice { get; set; }

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
        /// 返利政策容器
        /// </summary>
        [NotMapped]
        public RebatePolicyInfo RebatePolicyContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RebatePolicy))
                    return new RebatePolicyInfo();
                return JsonConvert.DeserializeObject<RebatePolicyInfo>(this.RebatePolicy);
            }
            set
            {
                if (value == null) RebatePolicy = null;
                RebatePolicy = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 返利总额
        /// </summary>
        public decimal RebateTotalAmount { get; set; }

        /// <summary>
        /// 修改返利总额
        /// </summary>
        public decimal EditRebateTotalAmount { get; set; }

        /// <summary>
        /// 生产日期(财务审核填写的生产日期)
        /// </summary>
        public DateTime? ProductionDateByFinancial { get; set; }

        #endregion

        #region 聚合属性

        [ForeignKey("Note")]
        [MaxLength(64)]
        public string Note_Id { get; set; }
        /// <summary>
        /// 采购入库单
        /// </summary>
        public PurchaseInStockNote Note { get; set; }
        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        [ForeignKey("PurchaseRequisitionItem")]
        [MaxLength(64)]
        public string PurchaseRequisitionItem_Id { get; set; }
        /// <summary>
        /// 采购申请单项
        /// </summary>
        public PurchaseRequisitionItem PurchaseRequisitionItem { get; set; }

        [ForeignKey("AllocationNoteItem")]
        [MaxLength(64)]
        public string AllocationNoteItem_Id { get; set; }
        /// <summary>
        /// 调拨单项
        /// </summary>
        public AllocationNoteItem AllocationNoteItem { get; set; }

        #endregion

        #region 业务方法

        public void ExchangeEditInfo()
        {
            if (this.RebateTotalAmount > 0 && this.EditRebateTotalAmount == 0)
            {
                this.RebateType = RebateType.未返现;
                this.WhetherRebate = false;
            }
            else if (this.EditRebateTotalAmount > 0 && this.RebateTotalAmount == 0)
                this.RebateType = RebateType.现金返;

            this.Price = this.EditPrice;
            this.TotalAmount = this.EditTotalAmount;
            this.BarePrice = this.EditBarePrice;
            this.RebateTotalAmount = this.EditRebateTotalAmount;
            this.EditPrice = 0;
            this.EditTotalAmount = 0;
            this.EditBarePrice = 0;
            this.EditRebateTotalAmount = 0;
            this.SingleRebate = this.Price - this.BarePrice;
        }

        #endregion
    }
}
