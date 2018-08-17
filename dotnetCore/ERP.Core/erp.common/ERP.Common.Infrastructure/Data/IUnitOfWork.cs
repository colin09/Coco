using System;
using System.Collections.Generic;


namespace ERP.Common.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IContext Context { get; }
        T GetRepository<T>() where T : IRepository;
        IUnitOfWorkCompleteHandle Begin();
        void Commit();
        void Rollback();
    }
}
