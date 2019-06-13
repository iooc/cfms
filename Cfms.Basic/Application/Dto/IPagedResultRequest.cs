using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 分页查询起始位置声明接口
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        int? Start { get; set; }
    }
}
