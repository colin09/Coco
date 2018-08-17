using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ShopModule.ShopBaseNoteModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.StockBillModule
{
    [Table("BaseNoteInfo")]
    public class BaseNoteInfo : BaseEntity, IDeleted, IAggregationRoot
    {
        #region 基础属性

        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public NoteType NoteType { get; set; }

        /// <summary>
        /// 单据操作类型
        /// </summary>
        public NoteOperationType OperationType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public NoteAuditState AuditState { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [MaxLength(200)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        [MaxLength(64)]
        public string OperatorUserId { get; set; }

        [MaxLength(64)]
        public string OperatorUserName { get; set; }

        [MaxLength(32)]
        public string OperatorMobile { get; set; }

        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 盘点单编号
        /// </summary>
        [MaxLength(64)]
        public string TakeStockNoteNo { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }

        public List<BaseNote> Items { get; set; }

        public List<ShopBaseNote> ShopItems { get; set; }
        [ForeignKey("TakeStockNote")]
        [MaxLength(64)]
        public string TakeStockNote_Id { get; set; }
        public TakeStockNote TakeStockNote { get; set; }
        #endregion

        #region 业务方法

        public void CheckEdit()
        {
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");
        }
        public void CheckDelete()
        {
            if (this.IsDeleted) return;
         
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");
        }

        #endregion

    }
}
