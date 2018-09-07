using System;
using Microsoft.EntityFrameworkCore;

using com.mh.mysql.factory;
using com.mh.mysql.iservice;
using com.mh.mysql.repository;

namespace com.mh.mysql.repository.dbSession
{
    public class HuiSession : IDbSession
    {

        private readonly string dbName="Hui";
        public int SaveChange()
        {

            DbContext context = DbContextFactory.GetCurrentDbContext(dbName);
            return context.SaveChanges();
        }


        

        private IStoreEntityRepository _storeRepository;


        public IStoreEntityRepository StoreRepository
        {
            get
            {
                _storeRepository = _storeRepository ?? new StoreEntityRepository();
                return _storeRepository;
            }
        }








    }
}