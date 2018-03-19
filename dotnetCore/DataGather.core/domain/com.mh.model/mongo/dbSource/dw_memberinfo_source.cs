using System;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.mongo.mgBase;


namespace com.mh.model.mongo.dbSource
{

    [BsonIgnoreExtraElements]
    public class dw_memberinfo_source : MagicHorseStBase
    {
        //public ObjectId _id { set; get; }
        //public string id { set; get; }
        public string store_no { set; get; }

        public string memberid { set; get; }
        public string member_openid { set; get; }

        public string mobile { set; get; }
        public string name { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? birthday { set; get; }
        public string cardno { set; get; }
        /// <summary>
        /// 0:男，1:女，2:未知
        /// </summary>
        public string sex { set; get; }
        public int? level_id { set; get; }


        public int? is_valid { set; get; }
        public double? valid_amount { set; get; }
        public double? cumulative_amount { set; get; }
        public double? freeze_amount { set; get; }

        public string brand_code { set; get; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? follow_date { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? create_date { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? update_date { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime cal_dt { set; get; }


        public DateTime? _registTime => create_date;


        public int _sex
        {
            get
            {
                int val = 0;
                Int32.TryParse(sex, out val);
                return val;
            }
        }
        //public DateTime? _birthDay
        //{
        //    get
        //    {
        //        var time = DateTime.Now;
        //        if (DateTime.TryParse(birthday, out time))
        //            return time;
        //        return null;
        //    }
        //}



    }
}