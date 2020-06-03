using Cfms.Basic.Interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.Basic.Application.Dto
{
    /// <summary>
    /// 树形节点模型定义
    /// </summary>
    public interface ITreeOutputDto<T>
    {
        /// <summary>
        /// 树形节点文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// jsTree 插件判断是否存在子节点
        /// </summary>
        public List<T> Children { get; set; }
    }
}
