using System.Data.Entity;
using com.wx.sqldb.repository;
using com.wx.sqldb.factory;

namespace com.wx.sqldb
{
    public class HuiDbSession : IDbSession
    {
        private IUserEntityRepository _userRepository;
        private IRoleEntityRepository _roleRepository;
        private IStoreEntityRepository _storeRepository;
        private IRecommendRelationEntityRepository _recommendRelationRepository;
        private IOutsiteUserEntityRepository _outsiteUserRepository;
        private IResourceEntityRepository _resourceRepository;

        private IPositionConfigEntityRepository _positionConfigRepository;
        private IPositionItemEntityRepository _positionItemRepository;

        /*
        private IUserCountEntityRepository _userCountRepository;
        private ISalerIncomeEntityRepository _salerIncomRepository;
        private ISalerIncomeHistoryEntityRepository _salerIncomeHistoryRepository;
        private ISalerIncomeCashApplyEntityRepository _salerIncomeCashApplyRepository;
        private ISalerIncomeCashTransferEntityRepository _salerIncomeCashTransferRepository;
        */


        public IUserEntityRepository UserRepository
        {
            get
            {
                _userRepository = _userRepository ?? new UserEntityRepository();
                return _userRepository;
            }
        }
        public IRoleEntityRepository RoleRepository
        {
            get
            {
                _roleRepository = _roleRepository ?? new RoleEntityRepository();
                return _roleRepository;
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
        public IRecommendRelationEntityRepository RecommendRelationRepository
        {
            get
            {
                _recommendRelationRepository = _recommendRelationRepository ?? new RecommendRelationEntityRepository();
                return _recommendRelationRepository;
            }
        }
        public IOutsiteUserEntityRepository OutsiteUserRepository
        {
            get
            {
                _outsiteUserRepository = _outsiteUserRepository ?? new OutsiteUserEntityRepository();
                return _outsiteUserRepository;
            }
        }
        public IResourceEntityRepository ResourceRepository
        {
            get
            {
                _resourceRepository = _resourceRepository ?? new ResourceEntityRepository();
                return _resourceRepository;
            }
        }
        public IPositionConfigEntityRepository PositionConfigRepository
        {
            get
            {
                _positionConfigRepository = _positionConfigRepository ?? new PositionConfigEntityRepository();
                return _positionConfigRepository;
            }
        }
        public IPositionItemEntityRepository PositionItemRepository
        {
            get
            {
                _positionItemRepository = _positionItemRepository ?? new PositionItemEntityRepository();
                return _positionItemRepository;
            }
        }

        /*
        public IUserCountEntityRepository UserCountRepository
        {
            get
            {
                _userCountRepository = _userCountRepository ?? new UserCountEntityRepository();
                return _userCountRepository;
            }
        }
        public ISalerIncomeEntityRepository SalerIncomRepository
        {
            get
            {
                _salerIncomRepository = _salerIncomRepository ?? new SalerIncomeEntityRepository();
                return _salerIncomRepository;
            }
        }
        public ISalerIncomeHistoryEntityRepository SalerIncomeHistoryRepository
        {
            get
            {
                _salerIncomeHistoryRepository = _salerIncomeHistoryRepository ?? new SalerIncomeHistoryEntityRepository();
                return _salerIncomeHistoryRepository;
            }
        }
        public ISalerIncomeCashApplyEntityRepository SalerIncomeCashApplyRepository
        {
            get
            {
                _salerIncomeCashApplyRepository = _salerIncomeCashApplyRepository ?? new SalerIncomeCashApplyEntityRepository();
                return _salerIncomeCashApplyRepository;
            }
        }
        public ISalerIncomeCashTransferEntityRepository SalerIncomeCashTransferRepository
        {
            get
            {
                _salerIncomeCashTransferRepository = _salerIncomeCashTransferRepository ?? new SalerIncomeCashTransferEntityRepository();
                return _salerIncomeCashTransferRepository;
            }
        }
        */















        public int SaveChange()
        {
            DbContext context = DbContextFactory.GetCurrentDbContext("");
            return context.SaveChanges();
        }

    }
}
