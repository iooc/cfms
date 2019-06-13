using Cfms.Basic.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Dto
{
    /// <summary>
    /// 分页查询条件传输对象模型
    /// </summary>
    public class PagingInputDto : IPagedResultRequest
    {
        /// <summary>
        /// 查询起始记录位置
        /// </summary>
        public int? Start { get; set; }
        /// <summary>
        /// 每页查询限制条数
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// 模糊查询条件
        /// </summary>
        public string Where { get; set; }
    }
}
