using Cfms.BPMN.Connectings;
using System.Collections.Generic;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 中间流对象定义接口
    /// </summary>
    public interface IIntermediateFlow : IFlow
    {
        /// <summary>
        /// 输入流唯一标识的集合
        /// </summary>
        List<SequenceFlow> Incoming { get; set; }
        /// <summary>
        /// 输出流唯一标识的集合
        /// </summary>
        List<SequenceFlow> Outgoing { get; set; }
    }
}
