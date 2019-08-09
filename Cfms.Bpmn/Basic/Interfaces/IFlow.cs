using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 流程对象定义接口
    /// </summary>
    public interface IFlow : IBpmn
    {
        /// <summary>
        /// 每个流对象都需要一个所属进程的引用
        /// </summary>
        IProcess ProcessRef { get; }
    }
}
