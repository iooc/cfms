using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 流程接口的定义
    /// </summary>
    public interface IProcess : IBpmn, IList<IBpmn>, IDisposable
    {
        /// <summary>
        /// 可订阅此对象获取进程执行过程通知数据
        /// </summary>
        Subject<ExecutorEventArgs> ExecuteNote { get; set; }
    }
}
