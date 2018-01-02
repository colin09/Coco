using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class OutsiteUserEntity : BaseEntity
    {
        public int UserId { set; get; }
        public string OpenId { set; get; }
        public OutsiteType OutSiteType { set; get; }
        public string AccountNO { set; get; }

        public byte IsOauthed { set; get; }
    }



    public enum OutsiteType
    {
        Weixin,
        //QQ,
        //Sina
    }


}
