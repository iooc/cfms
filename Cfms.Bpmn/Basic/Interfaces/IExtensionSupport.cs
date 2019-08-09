using Cfms.BPMN.Connectings;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 支持进行扩展运算的对象
    /// </summary>
    public interface IExtensionSupport
    {
        bool IsAllowToSend { get; }
        Subject<bool> ChangeNote { get; set; }
        /// <summary>
        /// 输出流唯一标识的集合
        /// </summary>
        List<SequenceFlow> Outgoing { get; set; }
    }
}
