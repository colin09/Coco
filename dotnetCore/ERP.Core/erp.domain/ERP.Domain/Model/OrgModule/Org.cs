using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    [Table("Orgs")]
    public class Org : BaseEntity, INewId
    {
        #region 构造函数

        public Org()
        {
            Address = new OrgAddress();

            Enable = EnableState.启用;

            OrgType = OrgModule.OrgType.大区;
        }

        #endregion

        #region 基本属性

        ///<summary>
        ///名称
        ///</summary>
        [MaxLength(125)]
        public string Name { get; set; }
        public OrgType OrgType { get; set; }
   
        public EnableState Enable { get; set; }

        [MaxLength(64)]
        public string NewId { get; set; }

        #endregion

        #region 聚合关系

        /// <summary>
        /// 组织机构地址
        /// </summary>
        public OrgAddress Address { get; set; }

        #endregion

    }
}
