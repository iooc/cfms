using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Dto
{
    /// <summary>
    /// 树形查询输入模型
    /// </summary>
    public class TreeInputDto<TPrimaryKey> : PagingInputDto 
        where TPrimaryKey : struct
    {
        public TPrimaryKey? ParentId { get; set; }
    }
}
