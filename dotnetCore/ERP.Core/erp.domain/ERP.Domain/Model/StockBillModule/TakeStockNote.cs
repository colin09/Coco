using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.StockBillModule
{
    /// <summary>
    /// 库存盘点单据(新盘点单)
    /// </summary>
    [Table("TakeStockNote")]
    public class TakeStockNote : BaseEntity, IDeleted
    {
        #region 基础信息

        /// <summary>
        /// 盘点单类型
        /// </summary>
        public TakeStockTypeEnum TakeStockType { get; set; }

        public TakeStockAuditState AuditState { get; set; }

        [MaxLength(255)]
        public string AuditStateRemark { get; set; }

        /// <summary>
        /// 盘点人员
        /// </summary>
        [MaxLength(200)]
        public string TakeUsers { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        [MaxLength(64)]
        public string OperatorUserId { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [MaxLength(64)]
        public string Operator { get; set; }

        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary>
        [MaxLength(50)]
        public string NoteName { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        public DateTime TakeTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 盘点结果
        /// </summary>
        [MaxLength(200)]
        public string TakeResult { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsPrint { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsEntering { get; set; }
        #endregion

        #region 聚合属性
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }

        public List<TakeStockItem> Items { get; set; }
        [ForeignKey("TakeStockRecord")]
        [MaxLength(64)]
        public string TakeStockRecord_Id { get; set; }
        public TakeStockRecord TakeStockRecord { get; set; }
        #endregion

        #region 方法
        public void Delete()
        {
            if (this.IsDeleted) return;
            if (this.AuditState == TakeStockAuditState.审核通过 || this.AuditState == TakeStockAuditState.待审核)
                throw new BusinessException("审核通过的单据不允许删除！");

            this.IsDeleted = true;
        }

        public void Print()
        {
            if (this.AuditState == TakeStockAuditState.未开始)
            {
                this.AuditState = TakeStockAuditState.盘点中;
            }
        }

        public void Cancel()
        {
            if (this.IsDeleted) return;
            if (this.AuditState == TakeStockAuditState.审核通过)
                throw new BusinessException("审核通过的单据不允许取消！");

            this.AuditState = TakeStockAuditState.已取消;
        }


        public void HQCancel()
        {
            if (this.IsDeleted) return;

            this.AuditState = TakeStockAuditState.已取消;
        }
        #endregion

    }
}
