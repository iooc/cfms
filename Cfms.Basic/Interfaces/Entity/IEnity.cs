using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Interfaces.Entity
{
    /// <summary>
    /// 实体基类的声明
    /// </summary>
    /// <typeparam name="T">实体主键类型</typeparam>
    public interface IEnity<T> where T : struct
    {
        T Id { get; set; }
    }
}
