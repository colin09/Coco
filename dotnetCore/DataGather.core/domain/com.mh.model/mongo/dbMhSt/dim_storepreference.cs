using com.mh.model.mongo.mgBase;
using MongoDB.Bson.Serialization.Attributes;

namespace com.mh.model.mongo.dbMhSt
{
  [BsonIgnoreExtraElements]
    public class dim_storepreference : MagicHorseStBase
    {
        public int storeid { get; set; }//门店编号
        public int groupid { get; set; }//
        public int moduleid { get; set; }//模块编号 2001=算法1  2004=算法4
        public string modulename { get; set; }//算法1,//模块名称  算法4
        public int preference_type_id { get; set; }//偏好id
        public string preference_type_name { get; set; }//偏好名
        public int sub_preference_type_id { get; set; }//偏好内容id
        public string sub_preference_type_name { get; set; }//偏好内容名
        public string preference_value_min { get; set; }//偏好内容值最小值
        public string preference_value_max { get; set; }//偏好内容值最大值
    }
}
