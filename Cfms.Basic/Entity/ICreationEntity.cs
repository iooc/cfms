using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    /// <summary>
    /// 创建审计实体模型的声明
    /// </summary>
    /// <typeparam name="T">实体主键</typeparam>
    public interface ICreationEntity<T> : IEnity<T>
        where T : struct
    {
        /// <summary>
        /// 创建用户Id
        /// </summary>
        int CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
