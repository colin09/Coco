using com.wx.common.helper;
using com.wx.common.logger;
using com.wx.sqldb;
using com.wx.sqldb.data;
using com.wx.sqldb.factory;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace com.wx.message.messageHandler
{
    public class ScanQrHandler : IMessageHandler
    {
        private ILog _log = Logger.Current();
        private readonly HuiDbSession _db = new HuiDbSession();

        public bool Action(string message)
        {
            _log.Info($"action scanQr,message:{message}");
            var array = JsonConvert.DeserializeObject<string[]>(message);
            if (array == null)
                return false;
            var openId = array[0];
            var key = array[1];
            if (key.StartsWith("s-"))
                key = key.Substring(2);
            var keyVal = Convert.ToInt32(key);

            if (keyVal > 1 * 10000 * 10000 && keyVal < 2 * 10000 * 10000)
            {
                var storeId = keyVal % (1 * 10000 * 10000);
            }
            else
            {
                var storeId = keyVal;
                _log.Info($"action scanQr,storeId:{storeId}");

                var query = _db.RecommendRelationRepository.Where(r => r.FromId == storeId && r.RecommendId == openId && r.Status == DataStatue.Normal);
                if (query.Any())
                    return true;

                _db.RecommendRelationRepository.Create(new RecommendRelationEntity()
                {
                    FromId = storeId,
                    RecommendId = openId,
                    RecommendType = 1
                });
                _db.SaveChange();
            }


            return true;
        }
    }
}
