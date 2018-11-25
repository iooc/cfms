using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Dto
{
    /// <summary>
    /// 实体传输对象模型定义
    /// </summary>
    /// <typeparam name="T">实体主键类型</typeparam>
    public interface IEntityDto<T>
    {
        T Id { get; set; }
    }
}
