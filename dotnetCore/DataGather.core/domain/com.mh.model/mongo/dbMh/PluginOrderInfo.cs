using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

using com.mh.model.enums;
using com.mh.model.mongo.mgBase;
using com.mh.common.encrypt;
using System.Text.RegularExpressions;

namespace com.mh.model.mongo.dbMh
{

    // 百丽旗下 [？，15mins,思加图，涛博]
    [BsonIgnoreExtraElements]
    public class PluginOrderTxt : PluginOrderBase
    {
        /// <summary>
        /// 现价
        /// </summary>
        public string saleprice { set; get; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string brandName { set; get; }
        /// <summary>
        /// 颜色编码
        /// </summary>
        public string colorNO { set; get; }
        /// <summary>
        /// 折后额
        /// </summary>
        public string discOverPrice { set; get; }
        /// <summary>
        /// 活动编号
        /// </summary>
        public string promotionNO { set; get; }
        /// <summary>
        /// 用券类型
        /// </summary>
        public string couponType { set; get; }

        /// <summary>
        /// 面值或折扣
        /// </summary>
        public string buyAmount { set; get; }
        /// <summary>
        /// 可用券金额
        /// </summary>
        public string availableAmount { set; get; }
        /// <summary>
        /// 商场结算码
        /// </summary>
        public string billingcode { set; get; }
        /// <summary>
        /// 扣率代码
        /// </summary>
        public string discountCode { set; get; }
        /// <summary>
        /// 行记录修改时间
        /// </summary>
        public string recordUpdateTime { set; get; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ctime { set; get; }


        
        public string settleprice { set; get; }
        //public string mainid { set; get; }
        public string dtlid { set; get; }
    }

    // P+
    [BsonIgnoreExtraElements]
    public class PluginOrderPP : PluginOrderBase
    {
        public string productseason { set; get; }
        public string buyername { set; get; }               
    }

    //15mins
    [BsonIgnoreExtraElements]
    public class PluginOrderFF : PluginOrderBase
    {
        /// <summary>
        /// 现价
        /// </summary>
        public string saleprice { set; get; }
    }

    //gxg
    [BsonIgnoreExtraElements]
    public class PluginOrderGxg : PluginOrderBase
    {
        /// <summary>
        /// 商品条码
        /// </summary>
        public string productbarcode { set; get; }
        /// <summary>
        /// 商品年份
        /// </summary>
        public string productyear { set; get; }
        /// <summary>
        /// 1春	2夏	3秋	4冬 12春夏 13春秋 123春夏秋..(0表示不分季节或者没有季节选项)
        /// </summary>
        public string productseason { set; get; }
        /// <summary>
        /// 成交价
        /// </summary>
        public string saleprice { set; get; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public string totalamount { set; get; }
        /// <summary>
        /// 导购名字
        /// </summary>
        public string salername { set; get; }
        public string mobile { set; get; }
    }

    // 伊华欧秀
    [BsonIgnoreExtraElements]
    public class PluginOrderEva : PluginOrderBase
    {
        /// <summary>
        /// 商品条码
        /// </summary>
        public string productbarcode { set; get; }
        /// <summary>
        /// 商品年份
        /// </summary>
        public string productyear { set; get; }
        /// <summary>
        /// 1春	2夏	3秋	4冬 12春夏 13春秋 123春夏秋..(0表示不分季节或者没有季节选项)
        /// </summary>
        public string productseason { set; get; }
        /// <summary>
        /// 成交价
        /// </summary>
        public string saleprice { set; get; }
        public string itemprice { set; get; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public string totalamount { set; get; }
        /// <summary>
        /// 导购名字
        /// </summary>
        public string salername { set; get; }
        public string mobile { set; get; }


        public DateTime ctime { set; get; }

        public DateTime utime { set; get; }

        public override OperateResult1 _result
        {
            set
            {
            }
            get
            {
                return new OperateResult1()
                {
                    md5key = EncryptHelper.MD5Encrypt($"{ordercode}{sku}{productcode}{productbarcode}{color}{size}{quantity}{paymenttime}")//, 4, 8
                };
            }
        }
    }


    //莱尔斯丹
    [BsonIgnoreExtraElements]
    public class PluginOrderCommon : PluginOrderBase
    {
        public int indexno
        {
            set { }
            get
            {
                if (string.IsNullOrEmpty(remark))
                    return 0;
                Regex _regexTag = new Regex(@"神{1}(\d+)(?:;|；)", RegexOptions.Compiled);
                var matches = _regexTag.Matches(remark);
                var list = new List<int>();
                foreach (Match match in matches)
                {
                    int num = 0;

                    int.TryParse(match.Groups[1].ToString(), out num);
                    if (num > 0)
                        list.Add(num);
                }
                if (list.Count > 0)
                    return list[0];

                return 0;
            }
        }
    }

    //巴洛克
    [BsonIgnoreExtraElements]
    public class PluginOrderBrc : PluginOrderBase
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string brandName { set; get; }

        public string buyername { set; get; }

        public string loginname { set; get; }

    }









    /* ******************************************************************************************************************
        base ---> 
     */


    [BsonIgnoreExtraElements]
    public class PluginOrderBase : MagicHorseBase, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public int index { set; get; }
        public string writedate { set; get; }
        /*
        private string _writedata;
        public string writedate
        {
            set { _writedata = value; }
            get
            {
                DateTime oldDate = new DateTime(2016, 1, 1);
                DateTime newDate = DateTime.Now.Date;

                TimeSpan ts = newDate - oldDate;
                return $"{ts.Days}.{_writedata}";
            }
        }*/

        /// <summary>
        /// 订单号
        /// </summary>
        public string ordercode { set; get; }
        public string ordertype { set; get; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string businesstype { set; get; }


        public string productname { set; get; }
        public string productcode { set; get; }
        public string producttype { set; get; }
        public string sku { set; get; }
        public string color { set; get; }
        public string size { set; get; }


        public string brand { set; get; }
        public string brandcode { set; get; }
        public string category { set; get; }
        public string categoryId { set; get; }


        //public float quantity { set; get; }
        private string _quantity;
        //[BsonIgnore]
        public string quantityExt
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._quantity = "1";
                    return;
                }
                var val = -999f;
                if (float.TryParse(value, out val))
                    this._quantity = val.ToString();
                else
                    this._quantity = "-999";
            }
        }
        public float quantity
        {
            set { this._quantity = value.ToString(); }
            get
            {
                var val = 0f;
                float.TryParse(this._quantity, out val);
                return val;
            }
        }
        public string unitprice { set; get; }
        public string disount { set; get; }
        public string discountrate { set; get; }
        public string discountamount { set; get; }
        public string usescorenum { set; get; }






        public string paymentprice { set; get; }
        public double _paymentprice
        {
            set { }
            get
            {
                var val = 0d;
                double.TryParse(paymentprice, out val);
                return val;
            }
        }
        private string _paymentTime;
        public string paymenttime
        {
            set { _paymentTime = value; }
            get { return getDateTime(_paymentTime).ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        /*
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime _paymenttime
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(saletime))
                {
                    //saletime 支付时间：HH:mm:ss
                    var ptime = paymenttime;
                    var stime = getDateTime(saletime);

                    if (stime == DateTime.MinValue)
                        ptime = ptime.Substring(0, 11) + saletime;
                    else
                        ptime = ptime.Substring(0, 10) + stime.ToString("yyyy-MM-dd HH:mm:ss").Substring(10);

                    var payTime = getDateTime(ptime);
                    return payTime == DateTime.MinValue ? getDateTime(_paymentTime) : payTime;
                }
                return getDateTime(_paymentTime);
            }
        }*/
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime _paymenttime { set; get; }
        public string paymentmethod { set; get; }

        public string saletime { set; get; }







        public string couponcode { set; get; }
        private string _customerid;
        public string customerid
        {
            set { this._customerid = value; }
            get
            {
                if (string.IsNullOrEmpty(_customerid))
                    return "";
                return this._customerid.Trim();
            }
        }
        private string _openid;
        public string openid
        {
            set { this._openid = value; }
            get
            {
                if (string.IsNullOrEmpty(_openid))
                    return "";
                return this._openid.Trim();
            }
        }
        private string _vipcode;
        public string vipcode
        {
            set { this._vipcode = value; }
            get
            {
                if (string.IsNullOrWhiteSpace(_vipcode))
                    return "";
                return _vipcode.Trim();
            }
        }
        public string birthday { set; get; }
        public string vipscorenum { set; get; }
        public string vipleavel { set; get; }







        public string buyercode { set; get; }
        public string sectioncode { set; get; }
        public string storesection { set; get; }
        public string marketstore { set; get; }
        public string remark { set; get; }




        #region  --  自动属性  --


        public int groupid { set; get; }
        public int storeid { set; get; }
        public int sectionid { set; get; }
        public int userid { set; get; }
        public int buyeruserid { set; get; }

        public int scanorder { set; get; }
        public int usecoupon { set; get; }
        public string couponid { set; get; }





        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createtime { set; get; }

        public string promotionid { get; set; }
        public string promotionname { get; set; }

        public string submoduleid { get; set; }
        public string submodulename { get; set; }

        public string memberid { set; get; }


        public virtual OperateResult1 _result
        {
            set
            {
            }
            get
            {
                //TimeSpan ts = this._paymenttime - com.magicalhorse.fashion.common.ConfigManager.PluginOrderPaymentBaseDay;
                return new OperateResult1()
                {
                    //paydays = ts.Days,
                    md5key = EncryptHelper.MD5Encrypt($"{ordercode}{sku}{productcode}{color}{size}{paymenttime}")//, 4, 8
                };
            }
        }

        #endregion


        public DateTime SetPaymentTime()
        {
            if (!string.IsNullOrWhiteSpace(saletime))
            {
                //saletime 支付时间：HH:mm:ss
                var ptime = paymenttime;
                var stime = getDateTime(saletime);

                if (stime == DateTime.MinValue)
                    ptime = ptime.Substring(0, 11) + saletime;
                else
                    ptime = ptime.Substring(0, 10) + stime.ToString("yyyy-MM-dd HH:mm:ss").Substring(10);

                var payTime = getDateTime(ptime);
                return payTime == DateTime.MinValue ? getDateTime(_paymentTime) : payTime;
            }
            return getDateTime(_paymentTime);
        }



        public DateTime? _birtday
        {
            get
            {
                if (birthday == null || getDateTime(birthday) == DateTime.MinValue)
                    return null;
                return getDateTime(birthday);
            }
        }

        private DateTime getDateTime(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime?.Trim())) return DateTime.MinValue;
            try
            {
                DateTime value;
                if (DateTime.TryParse(dateTime, out value))
                    return value;

                Regex rx = new Regex(@"^\d{4}(\-|\/|\.)\d{1,2}(\-|\/|\.)\d{1,2}", RegexOptions.Compiled);
                if (!rx.IsMatch(dateTime.Trim()))
                    return DateTime.MinValue;

                if (DateTime.TryParse(dateTime, out value))
                {
                    return value;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }



        #region  --  补齐 mongodb  --

        // move to PluginOrderCommon ×8
        public string updatetime { set; get; }
        public string uptime { set; get; }
        public string ordercount { set; get; }
        public string inventory { set; get; }
        public string orderdeep { set; get; }
        public string style { set; get; }
        public string fashion { set; get; }
        public string sellout { set; get; }
        public string sellouttime { set; get; }

        public string gender { set; get; }
        public string age { set; get; }

        public string followtime { set; get; }
        public string ifunfollow { set; get; }
        public string registtime { set; get; }
        public string lastupdatetime { set; get; }

        public string floor { set; get; }

        public string customertags { set; get; }


        #endregion


    }


    public class OperateResult1
    {
        public float paydays { set; get; }

        public string md5key { set; get; }
    }


}