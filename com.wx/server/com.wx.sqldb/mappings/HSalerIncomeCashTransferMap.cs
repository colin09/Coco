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
   public class HSalerIncomeCashTransferMap : EntityTypeConfiguration<SalerIncomeCashTransferEntity>
    {
        public HSalerIncomeCashTransferMap()
        {
            this.ToTable("H_SalerIncomeCashTransfer");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            this.Property(h => h.SalerUserId).HasColumnName("SalerUserId").IsRequired();
            this.Property(h => h.CashApplyId).HasColumnName("CashApplyId").IsRequired();
            this.Property(h => h.Amount).HasColumnName("Amount").IsRequired();
            this.Property(h => h.TransferState).HasColumnName("TransferState").IsRequired();

            this.Property(h => h.TransferNO).HasColumnName("TransferNO");
            this.Property(h => h.ResultCode).HasColumnName("ResultCode");
            this.Property(h => h.ResultMsg).HasColumnName("ResultMsg");
            this.Property(h => h.ErrorCode).HasColumnName("ErrorCode");
            this.Property(h => h.Content).HasColumnName("Content");

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}

