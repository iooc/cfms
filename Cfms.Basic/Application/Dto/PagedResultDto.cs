using Cfms.Basic.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Dto
{
    public class PagedResultDto<T, TPrimaryKey> : IPagedResultDto<T, TPrimaryKey>
        where T : IEntityDto<TPrimaryKey>
        where TPrimaryKey : struct
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
