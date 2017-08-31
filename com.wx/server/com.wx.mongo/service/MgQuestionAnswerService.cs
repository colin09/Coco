using com.wx.mongo.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace com.wx.mongo.service
{
    public class MgQuestionAnswerService : BaseService<MgQuestionAnswer>
    {
        public Dictionary<string, string> GetSimpleList()
        {
            var list = Search();
            if (list == null)
                return null;
            var dic = list.GroupBy(g => new { g.pgDate, g.groom, g.bride })
                .ToDictionary(g => g.First().StringId, g => $"{g.Key.groom} & {g.Key.bride}  -  {g.Key.pgDate.ToString("yyyy-MM-dd")}");

            return dic;
        }


        public MgQuestionAnswer GetOneById(string id)
        {
            //var filter = Builders<MgQuestionAnswer>.Filter.Eq("_id", id);
            var filter = Builders<MgQuestionAnswer>.Filter.Eq(a => a._id, new ObjectId(id));

            return Search(filter).FirstOrDefault();
        }

    }
}
