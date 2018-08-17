using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.DisposedNoteModule
{
    /// <summary>
    /// 处理品单据子表（转入记录）
    /// </summary>
    public class DisposedInNote : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 转入时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 转入数量（小单位）
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 转出数量（小单位）
        /// </summary>
        public int OutCount { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public CommonAuditState AuditState { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        /// 
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("DisposedInNoteInfo")]
        public string DisposedInNoteInfo_Id { get; set; }
        public DisposedInNoteInfo DisposedInNoteInfo { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法
        /*
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="auditState"></param>
        /// <param name="remark"></param>
        /// <param name="context"></param>
        public void Audit(CommonAuditState auditState, string remark, ERPContext context)
        {
            if (auditState == CommonAuditState.审核通过)
            {
                var productStock = context.ProductStocks.FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
                if (productStock == null)
                {
                    throw new BusinessException("库存不足，无法转入处理品！");
                }
                productStock.DisposedIn(this.Count);
            }

            this.AuditState = auditState;
            this.Remark = remark;
            this.AuditTime = DateTime.Now;
        }*/

        #endregion
    }
}
