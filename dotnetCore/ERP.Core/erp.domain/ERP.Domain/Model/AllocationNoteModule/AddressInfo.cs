using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AllocationNoteModule
{
    public class AddressInfo : IValueObject
    {
        [MaxLength(32)]
        public string UserName { get; set; }

        [MaxLength(32)]
        public string MobileNo { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }
    }
}
