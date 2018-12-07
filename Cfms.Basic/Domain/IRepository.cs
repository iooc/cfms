using Cfms.Basic.DependencyInjection;
using Cfms.Basic.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cfms.Basic.Domain
{
    /// <summary>
    /// 仓储服务的接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Injectable(typeof(RepositoryBase<,>))]
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : IEnity<TPrimaryKey> 
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 按指定条件查询存储库中的所有实体的数目
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询存储库中所有实体的数目
        /// </summary>
        /// <returns></returns>
        Task<int> Count();
        /// <summary>
        /// 删除指定键值的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(TPrimaryKey id);
        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 删除给定的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Delete(TEntity entity);
        /// <summary>
        /// 获取符合条件的第一条记录
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取符合条件的第一条记录
        /// </summary>
        /// <param name="id">指定记录Id</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefault(TPrimaryKey id);
        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Get(TPrimaryKey id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        /// <summary>
        /// 插入数据并返回插入的Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertAndGetId(TEntity entity);
        /// <summary>
        /// 插入数据并返回插入的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);
        /// <summary>
        /// 修改指定键指定列的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAction"></param>
        /// <returns></returns>
        Task<TEntity> Update(TPrimaryKey id, Func<TEntity, Task> updateAction);
        /// <summary>
        /// 修改数据实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity);
        /// <summary>
        /// 获取指定条件数据的唯一实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate);
    }
}
