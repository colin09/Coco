using ERP.Domain.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 运行商
    /// </summary>
    [Table("Operators")]
    public class Operators : Org, IAggregationRoot
    {
        public Operators()
        {
            this.OrgType = OrgModule.OrgType.总部;
        }
        #region 基本属性

        #endregion

        #region 聚合属性

        ///// <summary>
        ///// 运营商相关的配置
        ///// </summary>
        //public List<OPSetting> Settings { get; set; }

        #endregion

    }
}
