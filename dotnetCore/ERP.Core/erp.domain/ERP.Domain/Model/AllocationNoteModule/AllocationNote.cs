using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.AllocationNoteModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using ERP.Domain.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.AllocationNoteModule
{
    public class AllocationNote : BaseEntity, IAggregationRoot
    {
        public AllocationNote()
        {
            this.FromUser = new UserInfo();
            this.ToUser = new UserInfo();
            this.ConsigneeInfo = new AddressInfo();
            this.ConsignorInfo = new AddressInfo();
            this.AllocationSettlementNotes = new List<AllocationSettlementNote>();
            this.Items = new List<AllocationNoteItem>();
            this.ConfirmUser = new UserInfo();
            this.LogisticInfo = new LogisticInfo();
        }

        #region 基本属性

        /// <summary>
        /// 调拨单编号
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public AllocationState State { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public AllocationBusinessType BusinessType { get; set; }

        /// <summary>
        /// 调拨类型
        /// </summary>
        public AllocationType Type { get; set; }

        /// <summary>
        /// 大单位数量
        /// </summary>
        public int SpecificationsCount { get; set; }

        /// <summary>
        /// 小单位数量
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// 入库大单位数量
        /// </summary>
        public int InStockSpecificationsCount { get; set; }
        /// <summary>
        /// 入库小单位数量
        /// </summary>
        public int InStockUnitCount { get; set; }

        /// <summary>
        /// 发货大单位数量
        /// </summary>
        public int OutStockSpecificationsCount { get; set; }
        /// <summary>
        /// 发货小单位数量
        /// </summary>
        public int OutStockUnitCount { get; set; }

        /// <summary>
        /// 调拨单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 调拨单采购总金额（只有供应商-城市）
        /// </summary>
        public decimal TotalPurchaseAmount { get; set; }

        /// <summary>
        /// 可能为空
        /// </summary>
        public UserInfo FromUser { get; set; }
        /// <summary>
        /// 可能为空
        /// </summary>
        public UserInfo ToUser { get; set; }

        /// <summary>
        /// 发货人信息
        /// </summary>
        public AddressInfo ConsignorInfo { get; set; }

        /// <summary>
        /// 收货人信息
        /// </summary>
        public AddressInfo ConsigneeInfo { get; set; }

        /// <summary>
        /// 单据创建人-对应 FromUser 或 ToUser 
        /// </summary>
        [MaxLength(64)]
        public string CreateUserId { get; set; }

        /// <summary>
        /// 需确认的城市Id（只有城际调拨  城市-城市）会有值
        /// </summary>
        [MaxLength(64)]
        public string ConfirmCityId { get; set; }
        /// <summary>
        /// 确认发货时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }
        /// <summary>
        /// 确认发货操作人
        /// </summary>
        public UserInfo ConfirmUser { get; set; }
        /// <summary>
        /// 物流信息
        /// </summary>
        public LogisticInfo LogisticInfo { get; set; }

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

        /// <summary>
        /// 其他货品存储字段（通过NotSkuGifts设置、读取）
        /// </summary>
        public string NotSkuGiftString { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        [MaxLength(255)]
        public string RefuseReason { get; set; }

        /// <summary>
        /// 同意时间
        /// </summary>
        public DateTime? AgreeTime { get; set; }


        #region 冗余字段

        /// <summary>
        /// 采购申请单编号
        /// </summary>
        [MaxLength(64)]
        public string PurchaseRequisitionNo { get; set; }

        #endregion

        #endregion

        #region 聚合属性
        [ForeignKey("Provider")]
        [MaxLength(64)]
        public string Provider_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }

        [ForeignKey("FromCity")]
        [MaxLength(64)]
        public string FromCity_Id { get; set; }
        /// <summary>
        /// 调出城市（可能是总部城市，也可能是其他城市）
        /// </summary>
        public City FromCity { get; set; }
        [ForeignKey("FromStoreHouse")]
        [MaxLength(64)]
        public string FromStoreHouse_Id { get; set; }
        /// <summary>
        /// 调出仓库
        /// </summary>
        public StoreHouse FromStoreHouse { get; set; }
        [ForeignKey("ToCity")]
        [MaxLength(64)]
        public string ToCity_Id { get; set; }
        /// <summary>
        /// 调入城市
        /// </summary>
        public City ToCity { get; set; }
        [ForeignKey("ToStoreHouse")]
        [MaxLength(64)]
        public string ToStoreHouse_Id { get; set; }
        /// <summary>
        /// 调入仓库
        /// </summary>
        public StoreHouse ToStoreHouse { get; set; }
        [ForeignKey("PurchaseRequisition")]
        [MaxLength(64)]
        public string PurchaseRequisition_Id { get; set; }
        /// <summary>
        /// 关联的采购申请单（只有大区联采会关联）
        /// </summary>
        public PurchaseRequisition PurchaseRequisition { get; set; }

        /// <summary>
        /// 调拨单项
        /// </summary>
        public List<AllocationNoteItem> Items { get; set; }

        /// <summary>
        /// 调拨结算单，调拨单最多有两个结算单
        /// </summary>
        public List<AllocationSettlementNote> AllocationSettlementNotes { get; set; }

        #endregion

        #region 业务方法

        public virtual void Cancel()
        {
            if (this.BusinessType == AllocationBusinessType.大区联采)
            {
                if (this.State != AllocationState.待发货)
                    throw new BusinessException("只有待发货调拨单可取消！");

                if (this.PurchaseRequisition == null)
                {
                    throw new BusinessException("未加载采购申请单信息，无法取消！");
                }

                foreach (var item in this.Items)
                {
                    item.PurchaseRequisitionItem.PurchaseCount -= item.Count;
                }

                this.PurchaseRequisition.UpdateState();
            }
            else if (this.BusinessType == AllocationBusinessType.城际调拨)
            {
                if (this.State != AllocationState.待确认)
                    throw new BusinessException("只有待确认调拨单可取消！");
            }

            this.State = AllocationState.已取消;
        }

        /// <summary>
        /// 城际调拨 - 同意调货
        /// </summary>
        /// <param name="reason"></param>
        public virtual void Refuse(string reason)
        {
            if (this.State != AllocationState.待确认)
                throw new BusinessException("当前状态不可拒绝！");

            if (this.BusinessType != AllocationBusinessType.城际调拨 || this.Type != AllocationType.城市到城市)
                throw new BusinessException("非城际调拨单据不可拒绝！");

            this.State = AllocationState.已拒绝;
            this.RefuseReason = reason;
        }

        public virtual void Agree(LogisticMode mode, decimal logisticsFee)
        {
            if (this.State != AllocationState.待确认)
                throw new BusinessException("当前状态不可同意调货！");

            if (this.BusinessType != AllocationBusinessType.城际调拨 || this.Type != AllocationType.城市到城市)
                throw new BusinessException("非城际调拨单据不可同意调货！");

            this.State = AllocationState.待发货;
            if (this.LogisticInfo == null) this.LogisticInfo = new LogisticInfo();
            this.LogisticInfo.LogisticMode = mode;
            this.LogisticInfo.LogisticsFee = logisticsFee;
            this.AgreeTime = DateTime.Now;
        }

        /// <summary>
        /// 更新单据状态
        /// </summary>
        public virtual void UpdateState()
        {
            if (this.State == AllocationState.待发货)
            {
                if (this.Items.Any(i => i.OutStockCount > 0))
                    this.State = AllocationState.收货中;
            }
            else if (this.State == AllocationState.收货中 || this.State == AllocationState.待结算)
            {
                if (this.Items.All(i => i.InStockCount == i.Count))
                {
                    this.State = AllocationState.待结算;
                }
                else
                {
                    this.State = AllocationState.收货中;
                }
            }
        }

        /// <summary>
        /// 更新单据金额相关信息(新增、编辑后调用）
        /// </summary>
        public virtual void UpdateNoteAmount()
        {
            if (this.Items == null || this.Items.Count == 0 ||
                this.Items.Any(i => i.Product == null)
                || this.Items.Any(i => i.Product.Info == null))
            {
                throw new BusinessException("信息加载不全，无法计算金额！");
            }

            this.TotalAmount = this.Items.Sum(i => i.SpecificationsPrice * i.Count / i.Product.Info.Desc.PackageQuantity);
            this.TotalPurchaseAmount = this.Items.Sum(i => i.PurchaseSpecificationsPrice * i.Count / i.Product.Info.Desc.PackageQuantity);
            this.Items.ForEach(i => i.UpdateAmount());
        }

        /// <summary>
        /// 更新调拨单 数量相关信息
        /// </summary>
        public virtual void UpdateNoteCountInfo()
        {
            if (this.Items == null || this.Items.Count == 0 ||
               this.Items.Any(i => i.Product == null)
               || this.Items.Any(i => i.Product.Info == null))
            {
                throw new BusinessException("信息加载不全，无法计算金额！");
            }

            this.SpecificationsCount = this.Items.Sum(i => i.Count / i.Product.Info.Desc.PackageQuantity);
            this.UnitCount = this.Items.Sum(i => i.Count % i.Product.Info.Desc.PackageQuantity);
            this.InStockSpecificationsCount = this.Items.Sum(i => i.InStockCount / i.Product.Info.Desc.PackageQuantity);
            this.InStockUnitCount = this.Items.Sum(i => i.InStockCount % i.Product.Info.Desc.PackageQuantity);
            this.OutStockSpecificationsCount = this.Items.Sum(i => i.OutStockCount / i.Product.Info.Desc.PackageQuantity);
            this.OutStockUnitCount = this.Items.Sum(i => i.OutStockCount % i.Product.Info.Desc.PackageQuantity);
        }

        /// <summary>
        /// 仅发货后调用
        /// </summary>
        /// <param name="context"></param>
        public virtual void Delivery()
        {
            this.State = AllocationState.收货中;
            UpdateNoteCountInfo();
           // InitSettlementNotes(context);
        }

        /*
        #region 结算单据相关
        /// <summary>
        /// 初始化结算单据，应在首次发货时调用
        /// </summary>
        public virtual void InitSettlementNotes(ERPContext context)
        {
            if (this.AllocationSettlementNotes.Count > 0 || context.AllocationSettlementNotes.Any(n => n.AllocationNote.Id == this.Id))
            {
                return;
            }
            switch (this.Type)
            {
                case AllocationType.供应商到城市:
                    InitProviderToCitySettlementNotes(context);
                    break;
                case AllocationType.供应商到总部:
                    InitProviderToHQSettlementNotes();
                    break;
                case AllocationType.总部到城市:
                    InitHQToCitySettlementNotes();
                    break;
                case AllocationType.城市到城市:
                    if (this.BusinessType == AllocationBusinessType.城际调拨)
                        break;
                    InitCityToCitySettlementNotes(context);
                    break;
                default:
                    return;
            }
        }
        /// <summary>
        /// 初始化供应商到城市的结算单
        /// </summary>
        private void InitProviderToCitySettlementNotes(ERPContext context)
        {
            if (this.Provider == null || this.ToCity == null)
                throw new BusinessException("城市信息未加载完整，无法初始化结算单！");
            var hq = this.FromCity ?? CityUtility.GetHQCity(context);
            if (hq == null) throw new BusinessException("未查询到总部城市信息！");
            var providerToHQNote = new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromProvider = this.Provider,
                ToCity = hq,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.供应商到总部,
                TotalAmount = this.TotalPurchaseAmount,
            };

            var hqToCityNote = new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromCity = hq,
                ToCity = this.ToCity,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.总部到城市,
                TotalAmount = this.TotalAmount,
            };
            this.AllocationSettlementNotes.Add(hqToCityNote);
            this.AllocationSettlementNotes.Add(providerToHQNote);
        }
        /// <summary>
        /// 初始化供应商到总部的结算单
        /// </summary>
        private void InitProviderToHQSettlementNotes()
        {
            this.AllocationSettlementNotes.Add(new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromProvider = this.Provider,
                ToCity = this.ToCity,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.供应商到总部,
                TotalAmount = this.TotalPurchaseAmount,
            });
        }
        /// <summary>
        /// 初始化总部到城市结算单
        /// </summary>
        private void InitHQToCitySettlementNotes()
        {
            this.AllocationSettlementNotes.Add(new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromCity = this.FromCity,
                ToCity = this.ToCity,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.总部到城市,
                TotalAmount = this.TotalAmount,
            });
        }
        /// <summary>
        /// 初始化城市到城市结算单
        /// </summary>
        private void InitCityToCitySettlementNotes(ERPContext context)
        {
            if (this.FromCity == null || this.ToCity == null)
                throw new BusinessException("城市信息未加载完整，无法初始化结算单！");
            var hq = CityUtility.GetHQCity(context);
            if (hq == null) throw new BusinessException("未查询到总部城市信息！");
            var cityToHQNote = new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromCity = this.FromCity,
                ToCity = hq,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.城市到总部,
                TotalAmount = this.TotalAmount,
            };

            var hqToCityNote = new AllocationSettlementNote()
            {
                AllocationNote = this,
                AllocationNoteNo = this.NoteNo,
                AllocationType = this.Type,
                BusinessTime = this.CreateTime,
                BusinessType = this.BusinessType,
                FromCity = hq,
                ToCity = this.ToCity,
                SettlementState = AllocationSettlementState.未结算,
                SettlementType = AllocationSettlementType.总部到城市,
                TotalAmount = this.TotalAmount,
            };
            this.AllocationSettlementNotes.Add(hqToCityNote);
            this.AllocationSettlementNotes.Add(cityToHQNote);
        }

        /// <summary>
        /// 当前结算单是否所有入库单据均已下推应付单完毕（包含虚拟入库单）
        /// </summary>
        /// <param name="purchaseInStockNotes">当前单据生成的所有入库单</param>
        /// <returns></returns>
        public virtual bool IsAllCreatePayableBills(List<PurchaseInStockNote> purchaseInStockNotes)
        {
            if (this.State != AllocationState.待结算 && this.State != AllocationState.已结算)
                return false;

            if (this.Type == AllocationType.供应商到城市)
                return purchaseInStockNotes.All(n => n.IsDownReason)
                    && (purchaseInStockNotes.Count(n => n.City.Id == this.ToCity.Id) ==
                    purchaseInStockNotes.Count(n => n.City.Id != this.ToCity.Id));
            //城市到城市，只有一笔非调入城市入库单（一次性发货）
            if (this.Type == AllocationType.城市到城市)
            {
                return purchaseInStockNotes.All(n => n.IsDownReason)
                    && purchaseInStockNotes.Count(n => n.City.Id != this.ToCity.Id) == 1;
            }

            return purchaseInStockNotes.All(n => n.IsDownReason);
        }

        #endregion
         */
        #endregion
    }
}
