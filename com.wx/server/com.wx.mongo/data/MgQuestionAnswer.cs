using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.mongo.data
{


    [BsonIgnoreExtraElements]
    public class MgQuestionAnswer : MgBaseModel
    {
        /// <summary>
        /// 新郎
        /// </summary>
        public string groom { set; get; }
        public string bride { set; get; }
        /// <summary>
        /// 拍摄日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime pgDate { set; get; }
        /// <summary>
        /// 拍摄套系
        /// </summary>
        public string pgStyle { set; get; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string channel { set; get; }

        /// <summary>
        /// 化妆师
        /// </summary>
        public string makeup { set; get; }
        /// <summary>
        /// 服装师
        /// </summary>
        public string dress { set; get; }
        /// <summary>
        /// 摄影师
        /// </summary>
        public string photo { set; get; }
        /// <summary>
        /// 摄影助理
        /// </summary>
        public string assistant { set; get; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string phone { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { set; get; }


        public string q_1_1 { set; get; }
        public string q_1_2 { set; get; }
        public string q_1_3 { set; get; }

        public string q_2_1 { set; get; }
        public string q_2_2 { set; get; }
        public string q_2_3 { set; get; }

        public string q_3_1 { set; get; }
        public string q_3_2 { set; get; }
        public string q_3_3 { set; get; }

        public string q_4_1 { set; get; }
        public string q_4_2 { set; get; }
        public string q_4_3 { set; get; }
        public string q_4_4 { set; get; }
        public string q_4_5 { set; get; }
        public string q_4_6 { set; get; }

        public string q_5_1 { set; get; }
        public string q_5_2 { set; get; }
        public string q_5_3 { set; get; }
        public string q_5_4 { set; get; }
        public string q_5_5 { set; get; }
        public string q_5_6 { set; get; }

        public string q_6_1 { set; get; }
        public string q_6_2 { set; get; }
        public string q_6_3 { set; get; }
        public string q_6_4 { set; get; }
        public string q_6_5 { set; get; }
        public string q_6_6 { set; get; }



        public string q_7_1 { set; get; }
        public string q_7_2 { set; get; }
        public string q_7_3 { set; get; }
        public string q_7_4 { set; get; }

        public string idea { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createTime { set; get; }


        public string _pgDate { get { return pgDate.ToString("yyyy-MM-dd"); } }
        public string _createTime { get { return createTime.ToString("yyyy-MM-dd"); } }
    }
}
