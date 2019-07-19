using Cfms.Basic.Entity;
using System;

namespace Cfms.Basic.Interfaces.Entity
{
    /// <summary>
    /// 删除审计实体模型的声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeletionEntity<T> : IEnity<T>, ISoftDelete
        where T: struct
    {
        /// <summary>
        /// 删除操作用户Id
        /// </summary>
        int DeleteUserId { get; set; }
        /// <summary>
        /// 删除操作时间
        /// </summary>
        DateTime DeleteTime { get; set; }
    }
}
