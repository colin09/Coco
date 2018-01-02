using com.wx.sqldb.data;

namespace com.wx.sqldb.repository
{
    public class UserEntityRepository : BaseRepository<UserEntity>, IUserEntityRepository
    {
    }
    public class StoreEntityRepository : BaseRepository<StoreEntity>, IStoreEntityRepository
    {
    }
    public class RoleEntityRepository : BaseRepository<RoleEntity>, IRoleEntityRepository
    {
    }
    public class RecommendRelationEntityRepository : BaseRepository<RecommendRelationEntity>, IRecommendRelationEntityRepository
    {
    }

    public class OutsiteUserEntityRepository : BaseRepository<OutsiteUserEntity>, IOutsiteUserEntityRepository
    {
    }
    public class ResourceEntityRepository : BaseRepository<ResourceEntity>, IResourceEntityRepository
    {
    }
    public class PositionConfigEntityRepository : BaseRepository<PositionConfigEntity>, IPositionConfigEntityRepository
    {
    }
    public class PositionItemEntityRepository : BaseRepository<PositionItemEntity>, IPositionItemEntityRepository
    {
    }

    /*
    public class UserCountEntityRepository : BaseRepository<UserCountEntity>, IUserCountEntityRepository
    {
    }

    public class SalerIncomeEntityRepository : BaseRepository<SalerIncomeEntity>, ISalerIncomeEntityRepository
    {
    }

    public class SalerIncomeHistoryEntityRepository : BaseRepository<SalerIncomeHistoryEntity>, ISalerIncomeHistoryEntityRepository
    {
    }

    public class SalerIncomeCashApplyEntityRepository : BaseRepository<SalerIncomeCashApplyEntity>, ISalerIncomeCashApplyEntityRepository
    {
    }

    public class SalerIncomeCashTransferEntityRepository : BaseRepository<SalerIncomeCashTransferEntity>, ISalerIncomeCashTransferEntityRepository
    {
    }
    */



}
