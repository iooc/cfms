using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Dto
{
    /// <summary>
    /// 输入传输对象模型定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityInputDto<T>
        where T : struct
    {
        T? Id { get; set; }
    }
}
