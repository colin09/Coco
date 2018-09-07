using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrgModule
{
    /// <summary>
    /// 负责人
    /// </summary>
    public class OrgHead : IValueObject
    {
        public OrgHead() { }

        ///<summary>
        ///负责人名称
        ///</summary>
        [MaxLength(64)]
        public string Name { get; set; }

        ///<summary>
        ///性别
        ///</summary>
        [MaxLength(16)]
        public string Gender { get; set; }

        ///<summary>
        ///身份证号码
        ///</summary>
        [MaxLength(64)]
        public string CardNO { get; set; }

        ///<summary>
        ///手机号码
        ///</summary>
        [MaxLength(32)]
        public string MobileNO { get; set; }

        ///<summary>
        ///固定电话
        ///</summary>
        [MaxLength(32)]
        public string PhoneNO { get; set; }

        ///<summary>
        ///邮箱
        ///</summary>
        [MaxLength(64)]
        public string Email { get; set; }
    }
}
