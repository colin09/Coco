using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CommonModule
{
    /// <summary>
    /// 位置
    /// </summary>
    public class Location : IValueObject
    {
        public Location() { }

        public Location(string province, string city, string county = null)
        {
            this.Province = province;
            this.City = city;
            this.County = county;
        }
        public string CityId { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [MaxLength(20)]
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [MaxLength(20)]
        public string City { get; set; }

        ///<summary>
        ///区县
        ///</summary>
        [MaxLength(20)]
        public string County { get; set; }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public Location Clone()
        {
            return new Location()
            {
                Province = this.Province,
                City = this.City,
                County = this.County,
            };
        }

        public override string ToString()
        {
            return Province + City + County;
        }
    }
}
