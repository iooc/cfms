using Cfms.Basic.Entity;
using Cfms.Basic.EntityFrameworkCore;
using Cfms.Basic.Interfaces.Domain;
using Cfms.Basic.Interfaces.Domain.Uow;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Domain
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEnity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        protected DbContext dbContext;
        public RepositoryBase(DbContext _dbContext,IUnitOfWork uow)
        {
            dbContext = _dbContext;

            CurrentUnitOfWork = uow;
            CurrentUnitOfWork.CurrentDbContext = _dbContext;
        }

        public IUnitOfWork CurrentUnitOfWork { get; set; }

        public virtual Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            //throw new NotImplementedException();
            var result = Task.Run(() =>
            {
                var query = GetAll();
                var count = query.Count(predicate);
                return count;
            });

            return result;
        }

        public virtual Task<int> Count()
        {
            var result = Task.Run(() =>
            {
                var query = GetAll();
                var count = query.Count();
                return count;
            });

            return result;
        }

        public virtual Task Delete(TPrimaryKey id)
        {
            //throw new NotImplementedException();
            return Delete(a => a.Id.Equals(id));
        }

        public virtual Task Delete(Expression<Func<TEntity, bool>> predicate)
        {
            //throw new NotImplementedException();
            return Task.Run(() =>
            {
                var query = GetAll();
                var dbs = query.Where(predicate);
                foreach (var db in dbs)
                {
                    if (db is ISoftDelete dt)
                        dt.IsDeleted = true;
                    else
                        dbContext.Remove(db);
                }
                //dbContext.SaveChanges();
            });
        }

        public virtual Task Delete(TEntity entity)
        {
            return new Task(() =>
            {
                if (entity is ISoftDelete dt)
                    dt.IsDeleted = true;
                else
                    dbContext.Remove(entity);
                //dbContext.SaveChanges();
            });
        }

        public virtual Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                return GetAll().FirstOrDefault(predicate);
            });
        }

        public virtual Task<TEntity> FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(a => a.Id.Equals(id));
        }

        public virtual Task<TEntity> Get(TPrimaryKey id)
        {
            return FirstOrDefault(a => a.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            var query = dbContext.Set<TEntity>().AsQueryable();
            return query;
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> Insert(TEntity entity)
        {
            return Task.Run(() => {
                dbContext.Add(entity);
                var result = dbContext.SaveChanges();
                if (result >= 1)
                    return entity;
                else
                    return null;
            });
        }

        public virtual Task<TPrimaryKey> InsertAndGetId(TEntity entity)
        {
            return Task.Run(() =>
            {
                var db = Insert(entity).Result;
                if (db != null)
                    return db.Id;
                else
                    return default;
            });
        }

        public virtual Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() =>
            {
                var query = GetAll().SingleOrDefault(predicate);
                return query;
            });
        }

        public virtual async Task<TEntity> Update(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await FirstOrDefault(id);
            await updateAction(entity);
            return entity;
        }

        public virtual Task<TEntity> Update(TEntity entity)
        {
            return Task.Run(() =>
            {
                dbContext.Update(entity);
                return entity;
            });
        }
    }
}
