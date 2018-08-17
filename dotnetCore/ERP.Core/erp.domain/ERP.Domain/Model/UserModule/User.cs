using ERP.Domain.Enums.UserModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.UserModule
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("User")]
    public class User : BaseEntity, IAggregationRoot,INewId
    {
        #region 基本属性

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        ///<summary>
        ///用户名
        ///</summary>
        [MaxLength(50)]
        [Required]
        public string UserName { get; set; }

        ///<summary>
        ///手机号码
        ///</summary>
        [MaxLength(50)]
        public string MobileNO { get; set; }

        ///<summary>
        ///真实姓名
        ///</summary>
        [MaxLength(50)]
        public string TrueName { get; set; }

        /// <summary>
        /// 企业用户状态
        /// </summary>
        public CompanyUserState State { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [MaxLength(64)]
        public string CompanyName { get; set; }

        [MaxLength(64)]
        public string NewId { get; set; }

        #endregion

        #region 聚合属性

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public City City { get; set; }

        #endregion
    }
}
