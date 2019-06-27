using AutoMapper;
using Cfms.Basic.Application.Dto;
using Cfms.Basic.Domain;
using Cfms.Basic.Interfaces.Domain.Uow;
using Cfms.Basic.Interfaces.Dto;
using Cfms.Basic.Interfaces.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cfms.Basic.Application.Services
{
    /// <summary>
    /// 增删改查应用服务的基类
    /// </summary>
    /// <typeparam name="TEntity">查询实体类型</typeparam>
    /// <typeparam name="TEntityDto">输出传输对象类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TGetAllInput">查询条件类型</typeparam>
    /// <typeparam name="TCreateInput">插入条件类型</typeparam>
    /// <typeparam name="TUpdateInput">更新条件类型</typeparam>
    /// <typeparam name="TGetInput">单条查询条件类型</typeparam>
    /// <typeparam name="TDeleteInput">删除查询条件类型</typeparam>
    [Route("api/services/[controller]")]
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        : ApplicationService, ICrudAppService<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput, TGetInput, TDeleteInput>
        where TEntity : IEnity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
        where TUpdateInput : IEntityDto<TPrimaryKey>
        where TGetInput : IEntityDto<TPrimaryKey>
        where TDeleteInput : IEntityDto<TPrimaryKey>
        where TGetAllInput: IPagedResultRequest
    {
        /// <summary>
        /// 数据仓储
        /// </summary>
        protected IRepository<TEntity, TPrimaryKey> Repository;
        /// <summary>
        /// 当前工作单元
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork;
        /// <summary>
        /// 初始化增删改查应用服务
        /// </summary>
        /// <param name="repository"></param>
        public CrudAppService(IRepository<TEntity, TPrimaryKey> repository)
        {
            Repository = repository;

            CurrentUnitOfWork = repository.CurrentUnitOfWork;
        }
        /// <summary>
        /// 新增查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TEntityDto> Create([FromBody]TCreateInput input)
        {
            var entity = MapToEntity(input);

            await Repository.Insert(entity);
            await CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(entity);
        }
        /// <summary>
        /// 删除查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual Task Delete(TDeleteInput input)
        {
            Repository.Delete(input.Id);
            return CurrentUnitOfWork.SaveChanges();
        }
        /// <summary>
        /// 单条记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual async Task<TEntityDto> Get(TGetInput input)
        {
            var entity = await Repository.Get(input.Id);
            return MapToEntityDto(entity);
        }
        /// <summary>
        /// 符合条件的所有记录查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<IPagedResultDto<TEntityDto, TPrimaryKey>> GetAll(TGetAllInput input)
        {
            return Task.Run(() =>
            {
                var query = Repository.GetAll();

                var totalCount = query.Count();

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var result = new PagedResultDto<TEntityDto, TPrimaryKey>
                {
                    TotalCount = totalCount,
                    Data = query.Select(MapToEntityDto).ToList()
                };

                return result as IPagedResultDto<TEntityDto, TPrimaryKey>;
            });
        }
        /// <summary>
        /// 修改查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns
        [HttpPut]
        public virtual async Task<TEntityDto> Update(TUpdateInput input)
        {
            var entity = await Repository.Get(input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(entity);
        }
        /// <summary>
        /// 映射到数据传输对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TEntityDto MapToEntityDto(TEntity entity)
        {
            return Mapper.Map<TEntityDto>(entity);
        }
        /// <summary>
        /// 新增时映射到实体
        /// </summary>
        /// <param name="createInput"></param>
        /// <returns></returns>
        protected virtual TEntity MapToEntity(TCreateInput createInput)
        {
            return Mapper.Map<TEntity>(createInput);
        }
        /// <summary>
        /// 修改时映射到实体
        /// </summary>
        /// <param name="updateInput"></param>
        /// <param name="entity"></param>
        protected virtual void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
            Mapper.Map(updateInput, entity);
        }
        /// <summary>
        /// 应用分业查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetAllInput input)
        {
            //Try to use paging if available
            if (input is IPagedResultRequest pagedInput && input.Start.HasValue)
            {
                return query.Skip(pagedInput.Start.Value).Take(pagedInput.Limit.Value);
            }

            //Try to limit query result if available
            if (input is ILimitedResultRequest limitedInput && input.Limit.HasValue)
            {
                return query.Take(limitedInput.Limit.Value);
            }

            //No paging
            return query;
        }
        /// <summary>
        /// 应用排序查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetAllInput input)
        {
            //Try to sort query if available
            var sortInput = input as ISortedResultRequest;
            //if (sortInput != null)
            //{
            //    if (!string.IsNullOrWhiteSpace(sortInput.Sorting))
            //    {
            //        return query.OrderBy(sortInput.Sorting);
            //    }
            //}

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            //No sorting
            return query;
        }
    }
    /// <summary>
    /// 增删改查应用服务的基类(合并增删改条件模型)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TGetAllInput"></typeparam>
    /// <typeparam name="TInputDto"></typeparam>
    public abstract class CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TInputDto>
        : CrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TInputDto, TInputDto, TInputDto, TInputDto>
        where TEntity : IEnity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
        where TInputDto : IEntityDto<TPrimaryKey>
        where TGetAllInput : IPagedResultRequest
    {
        public CrudAppService(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }
    }
}
