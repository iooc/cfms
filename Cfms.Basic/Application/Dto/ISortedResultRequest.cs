using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 排序请求接口
    /// </summary>
    public interface ISortedResultRequest
    {
        /// <summary>
        /// 排序表达式与 Sql 相同
        /// </summary>
        string Sorting { get; set; }
    }
}
