using System;
﻿using ERP.Domain.Model.CommonModule;
﻿using ERP.Common.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Enums.CommonModule;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class Provider : BaseEntity
    {
        ///<summary>
        ///名称
        ///</summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// 供应商类型
        /// </summary>
        public ProviderType ProviderType { get; set; }

        /// <summary>
        /// 供应商属性
        /// </summary>
        public ProviderProperty ProviderProperty { get; set; }

        /// <summary>
        /// 是否已创建易经销账号
        /// </summary>
        public bool HasCreateYJXAccount { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditState AuditState { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        [MaxLength(255)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [MaxLength(64)]
        public string DepositBank { get; set; }

        /// <summary>
        /// 开户行名称
        /// </summary>
        [MaxLength(126)]
        public string BankName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [MaxLength(32)]
        public string AccountNumber { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        public string Remark { get; set; }

        /// <summary>
        /// 营业执照号码
        /// </summary>
        [MaxLength(64)]
        public string BusinessLicense { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public EnableState Enable { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public OrgHead Head { get; set; }

        /// <summary>
        /// 组织机构地址
        /// </summary>
        public OrgAddress Address { get; set; }

        /// <summary>
        /// 协议起始日期
        /// </summary>
        public DateTime? AgreementBeginTime { get; set; }
        /// <summary>
        /// 协议终止日期
        /// </summary>
        public DateTime? AgreementEndTime { get; set; }

        /// <summary>
        /// 是否需要协议
        /// </summary>
        public bool RequiredAgreement { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        /// <summary>
        /// 不占款（默认false 计算占款数据）
        /// </summary>
        public bool DonotTakeupMoney { get; set; }

        #region 聚合属性

        /// <summary>
        /// 所属城市
        /// </summary>
        public City City { get; set; }

        #endregion

        #region 业务方法

        public void UploadAgreement(DateTime beginTime, DateTime endTime)
        {
            this.AgreementBeginTime = beginTime;
            this.AgreementEndTime = endTime;
            this.AuditState = AuditState.待审核;
        }

        public void Audit(AuditState state)
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("已审核状态，无需再次审核！");
            this.AuditState = state;
        }

        #endregion
    }
}
