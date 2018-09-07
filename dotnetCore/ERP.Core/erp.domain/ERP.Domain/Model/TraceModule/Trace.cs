using ERP.Domain.Enums.TraceModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.TraceModule
{
    [Table("Trace")]
    public class Trace : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 关联的业务Id，根据TraceType判断
        /// </summary>
        [MaxLength(64)]
        public string BusinessId { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public TraceType TraceType { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public TraceAction TraceAction { get; set; }

        /// <summary>
        /// action 描述信息
        /// </summary>        
        public string Message { get; set; }

        /// <summary>
        /// 备注，用于存储拒绝、取消等操作时的原因等备注信息
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }
        [MaxLength(64)]
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [NotMapped]
        public string User_Name { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 操作人员
        /// </summary>
        public OrgUser User { get; set; }

        #endregion
    }
}
