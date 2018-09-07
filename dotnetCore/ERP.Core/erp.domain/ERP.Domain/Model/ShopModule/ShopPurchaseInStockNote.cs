using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ERP.Domain.Model.AuditTraceModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using ERP.Domain.Model.AllocationNoteModule;
using System.ComponentModel.DataAnnotations.Schema;

using ERP.Domain.Model.CommonModule;
using ERP.Domain.Enums.ShopModule;
using ERP.Domain.Enums.AuditTraceModule;

namespace ERP.Domain.Model.ShopModule
{
    /// <summary>
    /// 经销商入库单
    /// </summary>
    public class ShopPurchaseInStockNote : BaseEntity, IAggregationRoot, IDeleted
    {
        public ShopPurchaseInStockNote()
        {
            this.State = ShopPurchaseInStockNoteState.申请入库;
            this.IsDeleted = false;
            this.AuditTraces = new List<ShopPurchaseInStockNoteAuditTrace>();
            this.Items = new List<ShopInStockItem>();
        }

        #region 基本属性

        /// <summary>
        /// 采购单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        [Display(Name = "入库日期")]
        public DateTime InStockTime { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public ShopPurchaseInStockNoteState State { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 大单位数量
        /// </summary>
        public int SpecificationsCount { get; set; }

        /// <summary>
        /// 小单位数量
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// 已入库大单位数量
        /// </summary>
        public int InStockSpecificationsCount { get; set; }

        /// <summary>
        /// 已入库小单位数量
        /// </summary>
        public int InStockUnitCount { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int ExpiredDays { get; set; }

        /// <summary>
        /// 过期时间(不可用做Linq条件）
        /// </summary>
        [NotMapped]
        public DateTime? ExpiredDate
        {
            get
            {
                if (this.AuditTime.HasValue)
                    return this.AuditTime.Value.AddDays(this.ExpiredDays);
                return null;
            }
        }
        /// <summary>
        /// 审核通过时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        [MaxLength(256)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
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

        #endregion

        #region 聚合属性

        public List<ShopPurchaseInStockNoteAuditTrace> AuditTraces { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("Shop")]
        public string Shop_Id { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public Shop Shop { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }

        /// <summary>
        /// 入库明细
        /// </summary>
        public List<ShopInStockItem> Items { get; set; }

        #endregion

        #region 业务方法

        /// <summary>
        /// 审核（需加载AuditTraces）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="mobileNo"></param>
        /// <param name="userRoleCode"></param>
        /// <param name="remark"></param>
        /// <param name="passed"></param>
        public virtual void Audit(string userId, string userName, string mobileNo, string userRoleCode, string remark, bool passed)
        {
            if (this.State > 0)
            {
                throw new BusinessException("当前状态无需审核！");
            }
            ShopPurchaseInStockNoteAuditTrace trace;
            if (this.AuditTraces == null || this.AuditTraces.Count == 0)
            {
                trace = new ShopPurchaseInStockNoteAuditTrace()
                {
                    ShopPurchaseInStockNote = this,
                    TraceState = AuditTraceState.待审核,
                    UserRoleInfo = UserRoleInfo.PurchasingManager,
                };
            }
            else
            {
                //更新审核日志
                trace = this.AuditTraces.FirstOrDefault(t => t.UserRoleInfo.UserRole == userRoleCode);
            }
            if (trace != null)
            {
                trace.UserId = userId;
                trace.UserName = userName;
                trace.MobileNo = mobileNo;
                trace.Remark = remark;
                trace.TraceState = passed ? AuditTraceState.审核通过 : AuditTraceState.审核拒绝;
                trace.AuditTime = DateTime.Now;
            }
            else
            {
                //如果AuditTraces中找不到相应的UserRoleCode，说明无权审核。
                throw new BusinessException("用户无权审核当前单据！");
            }

            if (trace.TraceState == AuditTraceState.审核拒绝)
            {
                this.State = ShopPurchaseInStockNoteState.拒绝入库;
            }
            else
            {
                this.State = ShopPurchaseInStockNoteState.待入库;
            }

        }

        public virtual void UpdateState()
        {
            if (this.Items == null || this.Items.Count == 0)
            {
                throw new BusinessException("未加载经销商采购入库单项信息，无法更新状态！");
            }
            if (this.Items.Any(i => i.InStockNum > 0))
            {
                this.State = ShopPurchaseInStockNoteState.已入库;
            }
            else
            {
                if (this.ExpiredDate.HasValue && this.ExpiredDate.Value < DateTime.Now)
                {
                    if (this.Items.Any(i => i.InStockNum > 0))
                        this.State = ShopPurchaseInStockNoteState.待入库;
                    else
                        this.State = ShopPurchaseInStockNoteState.已过期;
                }
            }
        }

        public virtual void UpdateCountInfo()
        {
            if (this.Items == null || this.Items.Count == 0)
            {
                throw new BusinessException("未加载经销商采购入库单项信息，无法更新状态！");
            }

            if (this.Items.Any(i => i.Product == null) || this.Items.Any(i => i.Product.Info == null))
            {
                throw new BusinessException("未加载入库单项产品信息，无法计算数量信息！");
            }
            this.SpecificationsCount = this.Items.Sum(i => i.Num / i.Product.Info.Desc.PackageQuantity);
            this.UnitCount = this.Items.Sum(i => i.Num % i.Product.Info.Desc.PackageQuantity);
            this.InStockSpecificationsCount = this.Items.Sum(i => i.InStockNum / i.Product.Info.Desc.PackageQuantity);
            this.InStockUnitCount = this.Items.Sum(i => i.InStockNum % i.Product.Info.Desc.PackageQuantity);
        }

        // public virtual void Delete(ERPContext context)
        // {
        //     if (this.IsDeleted)
        //         return;

        //     this.IsDeleted = true;
        // }

        #endregion

    }
}
