using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 分页查询输出传输对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IPagedResultDto<T, TPrimaryKey> 
        where T : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 当前条件可查询结果总数
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// 当前分页数据
        /// </summary>
        List<T> Data { get; set; }
    }
}
