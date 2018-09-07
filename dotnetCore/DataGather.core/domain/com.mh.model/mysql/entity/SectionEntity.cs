using System;
using com.mh.model.enums;
using Microsoft.EntityFrameworkCore;

namespace com.mh.model.mysql.entity
{
    public partial class SectionEntity : BaseEntity
    {
        public string Location { get; set; }
        public string ContactPhone { get; set; }
        //public Nullable<DataStatus> Status { get; set; }
        public Nullable<int> StoreId { get; set; }
        //public Nullable<System.DateTime> CreateDate { get; set; }
        //public Nullable<int> CreateUser { get; set; }
        //public Nullable<System.DateTime> UpdateDate { get; set; }
        //public Nullable<int> UpdateUser { get; set; }
        public string ContactPerson { get; set; }
        public string Name { get; set; }
        //public int Id { get; set; }
        public string SectionCode { get; set; }

        /// <summary>
        /// 品牌Code
        /// </summary>
        public string BrandCode { get; set; }
        /// <summary>
        /// 销售品牌
        /// </summary>
        public string SaleBrand { get; set; }


        /// <summary>
        /// 门店简称
        /// </summary>
        public string ShortName { get; set; }



        public static void BuildMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SectionEntity>().ToTable("section");
        }

    }

}