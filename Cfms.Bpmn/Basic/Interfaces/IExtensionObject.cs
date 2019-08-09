using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 基于 BPMN 的扩展计算对象定义接口
    /// </summary>
    public interface IExtensionObject<T> where T : IExtensionSupport
    {
        /// <summary>
        /// 扩展对象的计算结果，为 True 表示同意继续下行
        /// </summary>
        bool ComputeResult { get; set; }
        /// <summary>
        /// 扩展对象父对象的引用
        /// </summary>
        T ParentObjectRef { get; }

        void Compute();
    }
}
