using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 结果显示限制接口
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// 限制显示条数
        /// </summary>
        int? Limit { get; set; }
    }
}
