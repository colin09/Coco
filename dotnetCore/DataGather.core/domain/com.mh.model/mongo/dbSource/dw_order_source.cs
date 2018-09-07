using System;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.mongo.mgBase;


namespace com.mh.model.mongo.dbSource
{
    [BsonIgnoreExtraElements]
    public class dw_order_source : MagicHorseStBase
    {

        public string dtl_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string size_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brand_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? qty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? tag_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? item_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ticket_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? settle_price { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? pay_time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string memberid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string assistant_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shop_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shop_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? order_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? business_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? item_flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? sale_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string color_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? disc_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pro_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ticket_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? available_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string billing_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_code { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? update_time { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime cal_dt { get; set; }
    }
}
