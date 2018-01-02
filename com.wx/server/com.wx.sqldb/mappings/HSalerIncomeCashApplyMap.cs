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
   public class HSalerIncomeCashApplyMap : EntityTypeConfiguration<SalerIncomeCashApplyEntity>
    {
        public HSalerIncomeCashApplyMap()
        {
            this.ToTable("H_SalerIncomeCashApply");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();


            this.Property(h => h.SalerUserId).HasColumnName("SalerUserId").IsRequired();
            this.Property(h => h.Amount).HasColumnName("Amount").IsRequired();
            this.Property(h => h.ApplyState).HasColumnName("ApplyState").IsRequired();
            this.Property(h => h.RetryCount).HasColumnName("RetryCount").IsRequired();
            this.Property(h => h.PaymentType).HasColumnName("PaymentType").IsRequired();


            this.Property(h => h.TransferFee).HasColumnName("TransferFee").IsRequired();
            this.Property(h => h.ErrorCode).HasColumnName("Description");
            this.Property(h => h.ErrorMsg).HasColumnName("ErrorMsg");

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }

}
