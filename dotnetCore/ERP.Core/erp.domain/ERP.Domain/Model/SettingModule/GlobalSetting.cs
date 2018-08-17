using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.SettingModule
{
    /// <summary>
    /// 全局配置信息
    /// </summary>
    public class GlobalSetting : BaseEntity, IAggregationRoot
    {
        [Required]
        [MaxLength(200)]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
