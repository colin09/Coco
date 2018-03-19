
/*
using System.Data.Entity.ModelConfiguration;

using com.mh.model.mysql.entity;

namespace com.mh.model.mysql.mapping
{
    public partial class StoreEntityMap : EntityTypeConfiguration<StoreEntity>
    {
        public StoreEntityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Description)
                .IsRequired();

            this.Property(t => t.Location)
                .IsRequired()
                .HasMaxLength(2048);

            this.Property(t => t.Tel)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.RMAAddress)
                .HasMaxLength(200);

            this.Property(t => t.RMAZipCode)
                .HasMaxLength(50);

            this.Property(t => t.RMAPerson)
                .HasMaxLength(10);

            this.Property(t => t.RMAPhone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Store");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Tel).HasColumnName("Tel");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.UpdatedUser).HasColumnName("UpdatedUser");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Group_Id).HasColumnName("Group_Id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.GpsLat).HasColumnName("GpsLat");
            this.Property(t => t.GpsLng).HasColumnName("GpsLng");
            this.Property(t => t.GpsAlt).HasColumnName("GpsAlt");
            this.Property(t => t.RMAAddress).HasColumnName("RMAAddress");
            this.Property(t => t.RMAZipCode).HasColumnName("RMAZipCode");
            this.Property(t => t.RMAPerson).HasColumnName("RMAPerson");
            this.Property(t => t.RMAPhone).HasColumnName("RMAPhone");
            this.Property(t => t.PaymentMethodType).HasColumnName("PaymentMethodType");
            this.Property(t => t.PaymentForBaseAccount).HasColumnName("PaymentForBaseAccount");
            this.Property(t => t.OpenDoorTime).HasColumnName("OpenDoorTime");
            this.Property(t => t.CloseDoorTime).HasColumnName("CloseDoorTime");
            this.Property(t => t.ServiceDesc).HasColumnName("ServiceDesc");
            this.Property(t => t.IsAutoCash).HasColumnName("IsAutoCash");
            this.Property(t => t.IsAutoRefunds).HasColumnName("IsAutoRefunds");
            this.Property(t => t.AutoCashStoreType).HasColumnName("AutoCashStoreType");
            this.Property(t => t.ShopNo).HasColumnName("ShopNo");
            this.Property(t => t.VTWEG).HasColumnName("VTWEG");
            this.Property(t => t.ProvinceId).HasColumnName("ProvinceId");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            Init();
        }

        partial void Init();
    }
}
 */