using ERP.Common.Infrastructure.Exceptions;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Repository.DapperRepository
{
    public static class IRepositoryExtensions
    {
        public static TEntity GetByIdOrNewId<TEntity>(this IRepository<TEntity> repository, string id)
            where TEntity : BaseEntity
        {
            var entity = repository.FirstOrDefault(id);
            if (entity != null) return entity;

            var entityType = typeof(TEntity);

            if (typeof(INewId).IsAssignableFrom(entityType))
            {
                var query = Common.Infrastructure.Dapper.QueryObject.Query.Create()
                  .Add(Criterion.Create("NewId", null, CriteriaOperator.IsNotNull))
                  .Add(Criterion.Create("NewId", id, CriteriaOperator.Equal));

                if (typeof(IDeleted).IsAssignableFrom(entityType))
                    query.Add(Criterion.Create("IsDeleted", false, CriteriaOperator.Equal));

                return repository.FirstOrDefault(query);
            }

            return default(TEntity);
        }

        public static TEntity GetByNewId<TEntity>(this IRepository<TEntity> repository, string idv2)
            where TEntity : BaseEntity
        {
            var entityType = typeof(TEntity);
            if (typeof(INewId).IsAssignableFrom(entityType))
            {
                var query = Common.Infrastructure.Dapper.QueryObject.Query.Create()
                  .Add(Criterion.Create("NewId", null, CriteriaOperator.IsNotNull))
                  .Add(Criterion.Create("NewId", idv2, CriteriaOperator.Equal));

                if (typeof(IDeleted).IsAssignableFrom(entityType))
                    query.Add(Criterion.Create("IsDeleted", false, CriteriaOperator.Equal));

                return repository.FirstOrDefault(query);
            }
            throw new BusinessException("不支持的方法！");
        }
    }
}
