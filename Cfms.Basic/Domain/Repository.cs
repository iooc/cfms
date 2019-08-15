using AutoMapper;
using Cfms.Basic.Entity;
using Cfms.Basic.Interfaces.Domain.Uow;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
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
        public RepositoryBase(DbContext _dbContext,IUnitOfWork uow,IMapper mapper)
        {
            dbContext = _dbContext;

            CurrentUnitOfWork = uow;
            CurrentUnitOfWork.CurrentDbContext = _dbContext;

            Mapper = mapper;
        }
        /// <summary>
        /// 当前工作单元
        /// </summary>
        public IUnitOfWork CurrentUnitOfWork { get; set; }
        /// <summary>
        /// 自动映射注入对象的引用
        /// </summary>
        public IMapper Mapper { get; private set; }
        /// <summary>
        /// 计算给定表达式条件的实体数
        /// </summary>
        /// <param name="predicate">条件表达式委托</param>
        /// <returns></returns>
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
        /// <summary>
        /// 计算实体总数
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 删除给定 ID 值的实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task Delete(TPrimaryKey id)
        {
            //throw new NotImplementedException();
            return Delete(a => a.Id.Equals(id));
        }
        /// <summary>
        /// 删除符合给定条件表达式的实体数
        /// </summary>
        /// <param name="predicate">条件表达式委托</param>
        /// <returns></returns>
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
            return Task.Run(() =>
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
            return ApplyDataFilters(query);
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
        /// <summary>
        /// 可重写的数据筛选器
        /// </summary>
        /// <typeparam name="TQueryable">可查询对象的类型</typeparam>
        /// <param name="query">可查询对象</param>
        /// <returns></returns>
        protected virtual TQueryable ApplyDataFilters<TQueryable>(TQueryable query)
           where TQueryable : IQueryable<TEntity>
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                query = (TQueryable)query.Where(e => ((ISoftDelete)e).IsDeleted == false);
            }

            //if (typeof(IMayTenant).IsAssignableFrom(typeof(TEntity)))
            //{
            //    var tenantId = CurrentTenant.Id;
            //    query = (TQueryable)query.Where(e => ((IMayTenant)e).TenantId == tenantId);
            //}

            return query;
        }
    }
}
