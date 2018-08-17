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
using ERP.Domain.Enums.PurchaseRequisitionModule;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.AuditTraceModule;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class PurchaseRequisition : BaseEntity, IAggregationRoot
    {
        public PurchaseRequisition()
            : base()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.AuditTraces = new List<PurchaseRequisitionAuditTrace>();
            this.Items = new List<PurchaseRequisitionItem>();
        }

        #region 基本属性

        /// <summary>
        /// 单据状态
        /// </summary>
        public PurchaseRequisitionState State { get; set; }

        /// <summary>
        /// 叫货状态
        /// </summary>
        public CallGoodsState CallGoodsState { get; set; }

        /// <summary>
        /// 供应商状态
        /// </summary>
        public PurchaseRequisitionProviderState ProviderState { get; set; }

        /// <summary>
        /// 采购类型
        /// </summary>
        public PurchaseRequisitionType RequisitionType { get; set; }

        /// <summary>
        /// 所有人员审核通过的时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime RequistitionTime { get; set; }

        /// <summary>
        /// 批次编号
        /// </summary>
        [MaxLength(64)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 申请单编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string RequisitionNo { get; set; }

        /// <summary>
        /// 申请单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public RequisitionPayType PayType { get; set; }

        /// <summary>
        /// 采购原因
        /// </summary>
        public PurchaseReason PurchaseReason { get; set; }

        /// <summary>
        /// 提货金额
        /// </summary>
        public decimal PickupAmount { get; set; }
        /// <summary>
        /// 预付未销金额
        /// </summary>
        public decimal UnWriteOffAmount { get; set; }
        /// <summary>
        /// 剩余核销天数
        /// </summary>
        public int WriteOffDays { get; set; }

        /// <summary>
        /// 几天后付款
        /// </summary>
        public int PayDays { get; set; }

        /// <summary>
        /// 预付金额（只有付款方式是预付款的才有值）
        /// </summary>
        public decimal PrepayAmount { get; set; }

        /// <summary>
        /// 是否创建了预付单（只有PayType为预付款的 使用该值）
        /// </summary>
        public bool HasCreatePrepayBill { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal HasPaymentAmount { get; set; }

        /// <summary>
        /// 大单位数量
        /// </summary>
        public int SpecificationsCount { get; set; }

        /// <summary>
        /// 小单位数量
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// 采购用户Id
        /// </summary>
        [MaxLength(64)]
        public string PurchaseUserId { get; set; }

        /// <summary>
        /// 采购人员
        /// </summary>
        [MaxLength(32)]
        public string PurchaseName { get; set; }
        /// <summary>
        /// 采购联系方式
        /// </summary>
        [MaxLength(32)]
        public string PurchaseMobileNo { get; set; }

        /// <summary>
        /// 录入人员角色名称
        /// </summary>
        [MaxLength(64)]
        public string PurchaseUserRoleName { get; set; }

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
                {
                    return this.AuditTime.Value.AddDays(this.ExpiredDays);
                }
                return null;
            }
        }

        /// <summary>
        /// 下一个需要审核的角色（审核中状态才有值，应与 AuditTraces中最后一条待审核记录同步）
        /// </summary>
        [MaxLength(256)]
        public string NeedAuditUserRole { get; set; }

        /// <summary>
        /// 下一个需要审核的角色名称
        /// </summary>
        [MaxLength(256)]
        public string NeedAuditUserRoleName { get; set; }

        /// <summary>
        /// 是否新供应商
        /// </summary>
        public bool IsNewProvider { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 采购审核清单 检查信息(只能对象赋值，不可更新属性等操作)
        /// </summary>
        [NotMapped]
        public PurchaseRequisitionCheckInfo CheckInfo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.CheckInfoJsonString))
                    return new PurchaseRequisitionCheckInfo();
                return JsonConvert.DeserializeObject<PurchaseRequisitionCheckInfo>(CheckInfoJsonString);
            }
            set
            {
                if (value == null)
                    CheckInfoJsonString = null;
                CheckInfoJsonString = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 检查信息数据（通过CheckInfoes更新、获取）
        /// </summary>
        public string CheckInfoJsonString { get; set; }

        [NotMapped]
        public NotSkuGift[] NotSkuGifts
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.NotSkuGiftString))
                    return new NotSkuGift[0];
                return JsonConvert.DeserializeObject<NotSkuGift[]>(NotSkuGiftString);
            }
            set
            {
                if (value == null)
                    NotSkuGiftString = null;
                NotSkuGiftString = JsonConvert.SerializeObject(value);
            }
        }

        public string NotSkuGiftString { get; set; }

        /// <summary>
        /// 拆单审核配置Id
        /// </summary>
        [MaxLength(64)]
        public string AuditSetting_Id { get; set; }
        /// <summary>
        /// 拆单类型
        /// </summary>
        public SeparateBillType SeparateBillType { get; set; }

        /// <summary>
        /// 抄送 用户Id 容器
        /// </summary>
        [NotMapped]
        public string[] CCOrgUserIdsContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CCOrgUserIds))
                {
                    return new string[0];
                }

                return JsonConvert.DeserializeObject<string[]>(CCOrgUserIds);
            }
            set { CCOrgUserIds = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// 抄送用户Id存储Json
        /// </summary>
        [MaxLength(516)]
        public string CCOrgUserIds { get; set; }


        /// <summary>
        /// 是否返利
        /// </summary>
        public bool WhetherRebate { get; set; }

        /// <summary>
        /// 核销日期(月为单位)
        /// </summary>
        public int VerificationDate { get; set; }


        /// <summary>
        /// 返利总额
        /// </summary>
        public decimal RebateTotalAmount { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 申请单项
        /// </summary>
        public List<PurchaseRequisitionItem> Items { get; set; }

        public List<PurchaseInStockNote> PurchaseInStockNotes { get; set; }

        /// <summary>
        /// 审核日志
        /// </summary>
        public List<PurchaseRequisitionAuditTrace> AuditTraces { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [MaxLength(64)]
        [ForeignKey("Provider")]
        public string Provider_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        /// 大区联采的调拨单
        /// </summary>
        public List<AllocationNote> AllocationNotes { get; set; }

        #endregion

        #region 业务方法
        /// <summary>
        /// 更新采购申请状态
        /// </summary>
        public virtual void UpdateState()
        {
            if (this.Items == null || this.Items.Count == 0)
            {
                throw new BusinessException("未加载采购申请单项信息，无法更新状态！");
            }
            var passedItems = this.Items.Where(i => i.AuditState == CommonAuditState.审核通过).ToArray();
            if (passedItems.Where(p => p.PurchaseRequisitionCount > p.PurchaseCount).Count() == 0)
            {
                this.State = PurchaseRequisitionState.已完成;
            }
            else
            {
                if (this.ExpiredDate.HasValue && this.ExpiredDate.Value < DateTime.Now)
                {
                    if (passedItems.Any(i => i.PurchaseCount > 0))
                        this.State = PurchaseRequisitionState.已完成;
                    else
                        this.State = PurchaseRequisitionState.已过期;
                }
                else
                {
                    this.State = PurchaseRequisitionState.交货中;
                }
            }
        }
        public virtual void Cancel()
        {
            if (this.State != PurchaseRequisitionState.审核中)
            {
                throw new BusinessException("只有审核中状态可撤销申请！");
            }

            var firstTrace = this.AuditTraces.FirstOrDefault(t => t.TraceState == AuditTraceState.发起申请);

            this.AuditTraces.RemoveAll(t => t.TraceState == AuditTraceState.待审核);

            this.AuditTraces.Add(new PurchaseRequisitionAuditTrace()
                {
                    TraceState = AuditTraceState.已取消,
                    UserId = firstTrace.UserId,
                    UserRoleInfo = firstTrace.UserRoleInfo,
                    AuditTime = DateTime.Now,
                    MobileNo = firstTrace.MobileNo,
                    SequenceNo = 1000,
                    UserName = firstTrace.UserName,
                    PurchaseRequisition = this,
                });

            this.State = PurchaseRequisitionState.已取消;
        }

        public virtual void Audit(string userId, string userName, string mobileNo, string userRoleCode, string remark, bool passed)
        {
            //更新审核日志
            var trace = this.AuditTraces.FirstOrDefault(t => t.UserRoleInfo.UserRole == userRoleCode);
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
                //如果NeedAuditUserRole的值 在 AuditTraces中无对应记录，视为数据异常
                throw new BusinessException("数据异常，请与技术人员联系！");
            }

            if (trace.TraceState == AuditTraceState.审核拒绝)
            {
                this.State = PurchaseRequisitionState.已拒绝;
                this.NeedAuditUserRole = null;
                this.NeedAuditUserRoleName = null;
                this.AuditTraces.RemoveAll(t => t.TraceState == AuditTraceState.待审核);
            }
            else
            {
                trace = this.AuditTraces.Where(t => t.TraceState == AuditTraceState.待审核)
                    .OrderBy(t => t.UserRoleInfo.Level).FirstOrDefault();
                if (trace == null)
                {
                    this.State = PurchaseRequisitionState.交货中;
                    this.ProviderState = PurchaseRequisitionProviderState.已审核;
                    this.AuditTime = DateTime.Now;
                    this.NeedAuditUserRole = null;
                    this.NeedAuditUserRoleName = null;
                }
                else
                {
                    this.NeedAuditUserRole = trace.UserRoleInfo.UserRole;
                    this.NeedAuditUserRoleName = trace.UserRoleInfo.UserRoleName;
                }
            }
        }

        public virtual void UpdateCountInfo()
        {
            if (this.Items == null || this.Items.Count == 0 ||
               this.Items.Any(i => i.Product == null)
               || this.Items.Any(i => i.Product.Info == null))
            {
                throw new BusinessException("信息加载不全，无法计算金额！");
            }

            this.SpecificationsCount = this.Items.Sum(i => i.PurchaseRequisitionCount / i.Product.Info.Desc.PackageQuantity);
            this.UnitCount = this.Items.Sum(i => i.PurchaseRequisitionCount % i.Product.Info.Desc.PackageQuantity);
        }
        #endregion
    }
}
