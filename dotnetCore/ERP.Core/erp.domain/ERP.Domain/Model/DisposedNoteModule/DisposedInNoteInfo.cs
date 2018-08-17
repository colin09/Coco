using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.DisposedNoteModule
{
    /// <summary>
    /// 处理品转入单据主表
    /// </summary>
    public class DisposedInNoteInfo : BaseEntity
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 转入时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        #region 聚合属性
        /// <summary>
        /// 转入单据
        /// </summary>
        public List<DisposedInNote> DisposedInNotes { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }

        #endregion
    }
}
