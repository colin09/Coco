using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 组织机构地址（指负责的区域）
    /// </summary>
    public class OrgAddress : IValueObject
    {
        ///<summary>
        ///省份
        ///</summary>
        [MaxLength(20)]
        public string Province { get; set; }

        ///<summary>
        ///城市
        ///</summary>
        [MaxLength(20)]
        public string City { get; set; }

        ///<summary>
        ///区县
        ///</summary>
        [MaxLength(20)]
        public string County { get; set; }

        [MaxLength(50, ErrorMessage = "详细地址最大长度不能超过50位！")]
        ///<summary>
        ///详细地址
        ///</summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 纬度值
        /// </summary>
        public double? Latitude { get; set; }


        /// <summary>
        /// 经度值
        /// </summary>
        public double? Longitude { get; set; }
        public override string ToString()
        {
            string strTmp = "";
            if (!string.IsNullOrEmpty(Province))
                strTmp += Province;
            if (!string.IsNullOrEmpty(City))
                strTmp += "-" + City;
            if (!string.IsNullOrEmpty(County))
                strTmp += "-" + County;
            if (!string.IsNullOrEmpty(DetailAddress))
                strTmp += " " + DetailAddress;
            return strTmp;
        }
    }
}
