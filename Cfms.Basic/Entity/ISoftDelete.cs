using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Entity
{
    /// <summary>
    /// 软删除接口声明
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 标识某条数据是否已被删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
