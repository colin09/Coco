using System;
using com.mh.common.extension;
using MongoDB.Bson.Serialization.Attributes;

namespace com.mh.model.mongo.dbMh
{

    [BsonIgnoreExtraElements]
    public class DateGroup
    {

        public DateGroup()
        {

        }
        public DateGroup(DateTime? dates)
        {
            if (dates.HasValue)
            {
                var date = dates.Value;
                this.Date = date.Date;
                this.Year = date.Year;
                this.Month = Convert.ToInt32($"{date.Year}{date.Month.ToString("00")}");
                this.Week = date.WeekOfYear();
                this.DayOdfYear = Convert.ToInt32($"{date.Year}{date.DayOfYear.ToString("000")}");
                this.Season = date.SeasonOfYear();
                this.DayOfWeek = (int)date.DayOfWeek + 1;
            }

        }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 天
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// 周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 季节
        /// </summary>
        public int Season { get; set; }

        public int DayOdfYear { get; set; }

        public int DayOfWeek { get; set; }

    }
}
