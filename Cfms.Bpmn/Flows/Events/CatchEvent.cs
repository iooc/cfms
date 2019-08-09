using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 捕获事件的基类
    /// </summary>
    public abstract class CatchEvent : FlowNode, IEvent
    {
        /// <summary>
        /// 输出流唯一标识的集合
        /// </summary>
        public List<SequenceFlow> Outgoing { get; set; }
        /// <summary>
        /// 事件定义属性
        /// </summary>
        public IStartEventDefinition EventDefinition { get; set; }
        /// <summary>
        /// 是否允许流程发送到下一步
        /// </summary>
        public bool IsAllowToSend
        {
            get
            {
                if (EventDefinition != null &&
                    EventDefinition.ExtensionCollection.Count > 0)
                {
                    return EventDefinition.ExtensionCollection.All(a => a.ComputeResult);
                }
                return true;
            }
        }

        public Subject<bool> ChangeNote { get; set; }
    }
}
