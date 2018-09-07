
using com.mh.mysql.iservice;
using com.mh.model.mysql.entity;


namespace com.mh.mysql.repository
{
    public interface IStoreEntityRepository : IBaseRepository<StoreEntity> { }
    public interface IGroupEntityRepository : IBaseRepository<GroupEntity> { }
    public interface ISectionEntityRepository : IBaseRepository<SectionEntity> { }
    public interface IOpcOrgInfoEntityRepository : IBaseRepository<OPC_OrgInfoEntity> { }
    public interface IImsOperatorEntityRepository : IBaseRepository<IMS_OperatorEntity> { }


    public interface IFifMinsCouponEntityRepository : IBaseRepository<Fifteen_Mins_CouponEntity> { }
    public interface IFifMinsPrivilegeCouponEntityRepository : IBaseRepository<Fifteen_Mins_Privilege_CouponEntity> { }

}