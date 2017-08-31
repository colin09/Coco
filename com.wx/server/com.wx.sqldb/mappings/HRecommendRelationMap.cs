using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.mappings
{
   public class HRecommendRelationMap : EntityTypeConfiguration<RecommendRelationEntity>
    {
        public HRecommendRelationMap()
        {
            this.ToTable("H_RecommendRelation");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            this.Property(h => h.FromId).HasColumnName("FromId").IsRequired();
            this.Property(h => h.RecommendId).HasColumnName("RecommendId").IsRequired();
            this.Property(h => h.RecommendType).HasColumnName("RecommendType").IsRequired();

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }

}
