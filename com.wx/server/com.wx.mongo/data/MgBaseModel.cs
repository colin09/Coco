using com.wx.common.config;
using MongoDB.Bson;
using System;

namespace com.wx.mongo.data
{
    public class MgBaseModel
    {
        // public int Id { set; get; }
        public ObjectId _id { get; set; }

        public string StringId
        {
            get
            {
                return _id.ToString();
            }
        }

        public ObjectId CreateObjectId()
        {
            return new ObjectId(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower());
        }
    }


    public class counters
    {
        public string _id { set; get; }
        public int seq { set; get; }
    }















    public static class MgBaseModelExt
    {
        public static string GetCollectionName(this MgBaseModel model)
        {
            return string.Format($"{AppSettingConfig.MgPrefix}{model.GetType().Name}");
        }
    }







}
