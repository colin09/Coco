using com.wx.common.helper;
using com.wx.sqldb;
using com.wx.sqldb.data;
using com.wx.sqldb.factory;
using Newtonsoft.Json;
using System.Linq;

namespace com.wx.message.messageHandler
{
    public class OutsiteHandler : IMessageHandler
    {
        private readonly HuiDbSession _db = new HuiDbSession();

        public bool Action(string message)
        {
            var array = JsonConvert.DeserializeObject<string[]>(message);
            if (array == null)
                return false;
            var openId = array[0];


            var query = _db.OutsiteUserRepository.Where(o => o.OpenId == openId && o.Status == DataStatue.Normal);
            if (query.Any())
                return true;

            _db.OutsiteUserRepository.Create(new OutsiteUserEntity()
            {
                UserId = 0,
                OpenId = openId,
                OutSiteType = OutsiteType.Weixin,
                AccountNO = Utility.GetSecond2015().ToString(),
                IsOauthed = 0
            });
            return true;
        }
    }
}
