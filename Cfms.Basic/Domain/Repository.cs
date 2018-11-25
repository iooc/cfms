using Cfms.Basic.Interfaces.Domain;
using Cfms.Basic.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Domain
{
    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : IEnity<TPrimaryKey>
    {
        public virtual Task<long> Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> Count()
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> FirstOrDefault(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Get(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TPrimaryKey> InsertAndGetId(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Update(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
