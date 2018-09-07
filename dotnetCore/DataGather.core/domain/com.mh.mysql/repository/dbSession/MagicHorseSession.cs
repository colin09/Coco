using System;
using Microsoft.EntityFrameworkCore;

using com.mh.mysql.factory;
using com.mh.mysql.iservice;
using com.mh.mysql.repository;

namespace com.mh.mysql.repository.dbSession
{
    public class MagicHorseSession : IDbSession
    {

        private readonly string dbName = "MagicHorse";
        public int SaveChange()
        {

            DbContext context = DbContextFactory.GetCurrentDbContext(dbName);
            return context.SaveChanges();
        }




        private IGroupEntityRepository _groupRepository;
        private IStoreEntityRepository _storeRepository;
        private ISectionEntityRepository _sectionRepository;
        private IOpcOrgInfoEntityRepository _orgInfoRepository;
        private IImsOperatorEntityRepository _imsOperatorRepository;

        private IFifMinsCouponEntityRepository _15minCouponRepository;
        private IFifMinsPrivilegeCouponEntityRepository _15minPrivilegeCouponRepository;


        public IGroupEntityRepository GroupRepository
        {
            get
            {
                _groupRepository = _groupRepository ?? new GroupEntityRepository();
                return _groupRepository;
            }
        }

        public IStoreEntityRepository StoreRepository
        {
            get
            {
                _storeRepository = _storeRepository ?? new StoreEntityRepository();
                return _storeRepository;
            }
        }
        public ISectionEntityRepository SectionRepository
        {
            get
            {
                _sectionRepository = _sectionRepository ?? new SectionEntityRepository();
                return _sectionRepository;
            }
        }
        public IOpcOrgInfoEntityRepository OrgInfoRepository
        {
            get
            {
                _orgInfoRepository = _orgInfoRepository ?? new OpcOrgInfoEntityRepository();
                return _orgInfoRepository;
            }
        }
        public IImsOperatorEntityRepository ImsOperatorRepository
        {
            get
            {
                _imsOperatorRepository = _imsOperatorRepository ?? new ImsOperatorEntityRepository();
                return _imsOperatorRepository;
            }
        }

        public IFifMinsCouponEntityRepository FifMinsCouponRepository
        {
            get
            {
                _15minCouponRepository = _15minCouponRepository ?? new FifMinsCouponEntityRepository();
                return _15minCouponRepository;
            }
        }

        public IFifMinsPrivilegeCouponEntityRepository FifMinsPrivilegeCouponRepository
        {
            get
            {
                _15minPrivilegeCouponRepository = _15minPrivilegeCouponRepository ?? new FifMinsPrivilegeCouponEntityRepository();
                return _15minPrivilegeCouponRepository;
            }
        }






    }
}