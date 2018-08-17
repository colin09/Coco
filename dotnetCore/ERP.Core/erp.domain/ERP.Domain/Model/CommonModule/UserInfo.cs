using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CommonModule
{
    public class UserInfo : IValueObject
    {
        [MaxLength(64)]
        public string UserId { get; set; }
        [MaxLength(32)]
        public string MobileNo { get; set; }
        [MaxLength(32)]
        public string UserName { get; set; }
    }
}
