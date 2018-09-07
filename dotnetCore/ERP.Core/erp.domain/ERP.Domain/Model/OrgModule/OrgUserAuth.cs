using ERP.Domain.Enums.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    public class OrgUserAuth : BaseEntity
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public OrgUserAuthType AuthType { get; set; }

        /// <summary>
        /// 组织机构Id，系统用户是总部，仓库用户是仓库，城市用户是城市，大区用户是  大区
        /// </summary>
        [MaxLength(64)]
        public string OrgId { get; set; }

        /// <summary>
        /// 组织机构类型
        /// </summary>
        public OrgType OrgType { get; set; }

        #region 聚合属性
        [ForeignKey("OrgUser")]
        [MaxLength(64)]
        public string OrgUser_Id { get; set; }
        public OrgUser OrgUser { get; set; }

        #endregion
    }
}
