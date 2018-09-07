using System;
using System.Linq;
using System.Collections.Generic;

using ERP.Domain.Context;
using ERP.Common.Infrastructure.Ioc;
using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Utility;

namespace ERP.Domain.Dapper
{
     public class DapperUnitOfWork : IUnitOfWork
    {
        internal IERPContext _context;
        private Dictionary<Type, IRepository> repositories = new Dictionary<Type, IRepository>();
        public DapperUnitOfWork(IERPContext context)
        {
            this._context = context;
        }

        public IContext Context { get { return _context; } }
        public T GetRepository<T>() where T : IRepository
        {
            if (repositories.ContainsKey(typeof(T)))
            {
                return (T)repositories[typeof(T)];
            }

            var repo = IocManager.Resolve<T>();
            repositories.Add(typeof(T), repo);
            return repo;
        }
        public IUnitOfWorkCompleteHandle Begin()
        {
            if (_context.IsTransactionStarted)
            {
                Rollback();
            } // throw new InvalidOperationException("已开启事务.");
            _context.BeginTransaction();
            return new UnitOfWorkCompleteHandle(this);
        }
        public void Commit()
        {
            if (!_context.IsTransactionStarted)
                throw new InvalidOperationException("事务已提交或被释放.");

            _context.Commit();
        }
        public void Rollback()
        {
            if (!_context.IsTransactionStarted)
                throw new InvalidOperationException("当前无事务.");
            _context.Rollback();
        }

        #region IDisposed

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}