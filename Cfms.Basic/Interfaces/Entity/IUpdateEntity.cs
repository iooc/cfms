using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    /// <summary>
    /// 修改审计实体模型声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateEntity<T> : IEnity<T>
    {
        /// <summary>
        /// 修改操作用户Id
        /// </summary>
        int UpdateUserId { get; set; }
        /// <summary>
        /// 修改操作时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
