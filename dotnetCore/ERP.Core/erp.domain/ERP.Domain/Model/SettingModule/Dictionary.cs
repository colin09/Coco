using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.SettingModule
{
    public class Dictionary : BaseEntity
    {
        #region 基本属性
        public string Name { get; set; }

        public string Code { get; set; }

        public string TypeCode { get; set; }

        public int Sequence { get; set; }

        public string Desciption { get; set; }

        public bool IsDictionaryType { get; set; }

        public string ImageId { get; set; }

        #endregion

        #region 聚合属性
        public Dictionary Parent { get; set; }

        public List<Dictionary> Children { get; set; }
        #endregion
    }

}
