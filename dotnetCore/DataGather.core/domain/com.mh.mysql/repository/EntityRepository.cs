
using com.mh.mysql.iservice;
using com.mh.model.mysql.entity;

namespace com.mh.mysql.repository
{
    public class GroupEntityRepository : BaseRepository<GroupEntity>, IGroupEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
    public class StoreEntityRepository : BaseRepository<StoreEntity>, IStoreEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
    public class SectionEntityRepository : BaseRepository<SectionEntity>, ISectionEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
    public class OpcOrgInfoEntityRepository : BaseRepository<OPC_OrgInfoEntity>, IOpcOrgInfoEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
    public class ImsOperatorEntityRepository : BaseRepository<IMS_OperatorEntity>, IImsOperatorEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }


    public class FifMinsCouponEntityRepository : BaseRepository<Fifteen_Mins_CouponEntity>, IFifMinsCouponEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
    public class FifMinsPrivilegeCouponEntityRepository : BaseRepository<Fifteen_Mins_Privilege_CouponEntity>, IFifMinsPrivilegeCouponEntityRepository
    {
        protected override string dbName => "MagicHorse";
    }
}