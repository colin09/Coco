using ERP.Domain.Model.AuditTraceModule;
using ERP.Domain.Model.OrgModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Enums.ShopModule;

namespace ERP.Domain.Model.ShopModule
{
    /// <summary>
    /// 经销商提货单
    /// </summary>
    public class ShopPickupNote : BaseEntity, IAggregationRoot
    {
        public ShopPickupNote()
            : base()
        {
            this.Items = new List<ShopPickupNoteItem>();
        }

        #region 基本属性

        /// <summary>
        /// 单据状态
        /// </summary>
        public ShopPickupNoteState State { get; set; }

        /// <summary>
        /// 预计提货时间
        /// </summary>
        public DateTime PickupTime { get; set; }

        /// <summary>
        /// 申请单编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 大单位数量
        /// </summary>
        public int SpecificationsCount { get; set; }

        /// <summary>
        /// 小单位数量
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [MaxLength(64)]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建人手机号码
        /// </summary>
        [MaxLength(32)]
        public string CreateUserMobileNo { get; set; }
        /// <summary>
        /// 创建人名字
        /// </summary>
        [MaxLength(64)]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 申请单项
        /// </summary>
        public List<ShopPickupNoteItem> Items { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// <summary>
        /// 仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("Shop")]
        public string Shop_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Shop Shop { get; set; }

        #endregion

        #region 业务方法
        /// <summary>
        /// 更新状态
        /// </summary>
        public virtual void UpdateState()
        {
            if (this.Items == null || this.Items.Count == 0)
            {
                throw new BusinessException("未加载采购申请单项信息，无法更新状态！");
            }
            //有提货即完成状态
            if (this.Items.Any(p => p.PickupCount > 0))
            {
                this.State = ShopPickupNoteState.已完成;
            }
            else
            {
                this.State = ShopPickupNoteState.待提货;
            }
        }
        /// <summary>
        /// 取消提货单
        /// </summary>
        public virtual void Cancel()
        {
            if (this.State != ShopPickupNoteState.待提货)
            {
                throw new BusinessException("只有待提货状态可撤销！");
            }

            this.State = ShopPickupNoteState.已取消;
        }

        public virtual void UpdateCountInfo()
        {
            if (this.Items == null || this.Items.Count == 0 ||
               this.Items.Any(i => i.Product == null)
               || this.Items.Any(i => i.Product.Info == null))
            {
                throw new BusinessException("信息加载不全，无法计算金额！");
            }

            this.SpecificationsCount = this.Items.Sum(i => i.Count / i.Product.Info.Desc.PackageQuantity);
            this.UnitCount = this.Items.Sum(i => i.Count % i.Product.Info.Desc.PackageQuantity);
        }
        #endregion
    }
}
