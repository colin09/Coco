using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class ResourceEntity : BaseEntity
    {
        public ResourceEntity() { }



        public int SourceId { get; set; }
        public SourceType SourceType { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string ExtName { get; set; }
        public int SortOrder { get; set; } = 1;
        public int ExtId { get; set; } = 0;
        public bool IsDefault { get; set; } = false;



    }



    public enum SourceType
    {
        StoreQrCode=1
    }

}
