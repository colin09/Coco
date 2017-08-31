using com.wx.sqldb.data;
namespace com.wx.sqldb.repository
{
    public interface IUserEntityRepository : IBaseRepository<UserEntity> { }
    public interface IStoreEntityRepository : IBaseRepository<StoreEntity> { }
    public interface IRoleEntityRepository : IBaseRepository<RoleEntity> { }
    public interface IRecommendRelationEntityRepository : IBaseRepository<RecommendRelationEntity> { }
    public interface IOutsiteUserEntityRepository : IBaseRepository<OutsiteUserEntity> { }
    public interface IResourceEntityRepository : IBaseRepository<ResourceEntity> { }
    public interface IPositionConfigEntityRepository : IBaseRepository<PositionConfigEntity> { }
    public interface IPositionItemEntityRepository : IBaseRepository<PositionItemEntity> { }

    /*
    public interface IUserCountEntityRepository : IBaseRepository<UserCountEntity> { }
    public interface ISalerIncomeEntityRepository : IBaseRepository<SalerIncomeEntity> { }
    public interface ISalerIncomeHistoryEntityRepository : IBaseRepository<SalerIncomeHistoryEntity> { }
    public interface ISalerIncomeCashApplyEntityRepository : IBaseRepository<SalerIncomeCashApplyEntity> { }
    public interface ISalerIncomeCashTransferEntityRepository : IBaseRepository<SalerIncomeCashTransferEntity> { }
    */


}