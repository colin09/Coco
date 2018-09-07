using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CommonModule
{
    /// <summary>
    /// 地址
    /// </summary>
    public class Address : IValueObject
    {
        ///<summary>
        ///省份
        ///</summary>
        [MaxLength(32)]
        public string Province { get; set; }

        ///<summary>
        ///城市
        ///</summary>
        [MaxLength(32)]
        public string City { get; set; }

        ///<summary>
        ///区县
        ///</summary>
        [MaxLength(32)]
        public string County { get; set; }

        ///<summary>
        ///详细地址
        ///</summary>
        [MaxLength(256)]
        public string DetailAddress { get; set; }
    }
}
