using System;


namespace ERP.Common.Infrastructure.Data
{
    public class UnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        private IUnitOfWork _uow;
        public UnitOfWorkCompleteHandle(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_uow.Context.IsTransactionStarted)
                        _uow.Rollback();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
