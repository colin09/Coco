using ERP.Domain.Context;
using ERP.Domain.Enums.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 组织机构用户(系统运维相关人员)
    /// </summary>
    [Table("OrgUser")]
    public class OrgUser : BaseEntity, IAggregationRoot, INewId
    {
        public OrgUser()
        {
            Auths = new List<OrgUserAuth>();
        }

        #region 基本属性

        ///<summary>
        ///用户名
        ///</summary>
        ///<remarks>
        /// 只有管理员才使用用户名登录
        /// </remarks>
        [MaxLength(50)]
        public string UserName { get; set; }

        ///<summary>
        ///密码,MD5 32位加密存储
        ///</summary>
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }

        ///<summary>
        ///手机号码
        ///</summary>
        [MaxLength(20)]
        [Required]
        public string MobileNO { get; set; }

        ///<summary>
        ///真实姓名
        ///</summary>
        [MaxLength(50)]
        [Required]
        public string TrueName { get; set; }

        ///<summary>
        ///最后登录日期
        ///</summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 停用和启用
        /// </summary>
        public EnableState Enable { get; set; }

        [MaxLength(64)]
        public string NewId { get; set; }

        #endregion

        #region 聚合关系

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        public List<OrgUserAuth> Auths { get; set; }

        #endregion
    }
}
