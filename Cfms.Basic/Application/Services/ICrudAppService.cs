using Cfms.Basic.Interfaces.Dto;
using System.Threading.Tasks;

namespace Cfms.Basic.Application.Services
{
    /// <summary>
    /// 增删该查服务接口的声明
    /// </summary>
    /// <typeparam name="TEntityDto">输出传输对象</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    /// <typeparam name="TGetAllInput">查询条件传输模型类型</typeparam>
    /// <typeparam name="TCreateInput">新增传输模型</typeparam>
    /// <typeparam name="TUpdateInput">修改传输模型</typeparam>
    /// <typeparam name="TGetInput">查询条件传输模型类型</typeparam>
    /// <typeparam name="TDeleteInput">删除条件传输模型</typeparam>
    public interface ICrudAppService
        <TEntityDto, TPrimaryKey, in TGetAllInput, in TCreateInput, in TUpdateInput, in TGetInput, in TDeleteInput>
        :IAppService
        where TEntityDto: IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 新增数据服务接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Create(TCreateInput input);
        /// <summary>
        /// 删除数据服务接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(TDeleteInput input);
        /// <summary>
        /// 获取单条数据的服务接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Get(TGetInput input);
        /// <summary>
        /// 获取指定条件的所有数据服务接口
        /// </summary>
        /// <param name="input">查询条件传输对象</param>
        /// <returns></returns>
        Task<IPagedResultDto<TEntityDto, TPrimaryKey>> GetAll(TGetAllInput input);
        /// <summary>
        /// 修改数据的服务接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Update(TUpdateInput input);
    }
}
