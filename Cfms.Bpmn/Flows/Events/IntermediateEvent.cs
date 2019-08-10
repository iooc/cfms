using Cfms.BPMN.Basic.Interfaces;
using System.Linq;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 中间捕获事件（指向元素的事件）
    /// </summary>
    public class IntermediateCatchEvent : IntermediateFlow, IEvent, IExtensionSupport
    {
        public IIntermediateCatchEventDefinition EventDefinition { get; set; }
        /// <summary>
        /// 边界事件定义
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
    /// <summary>
    /// 中间抛出事件（指出元素的事件）
    /// </summary>
    public class IntermediateThrowEvent : IntermediateFlow, IEvent, IExtensionSupport
    {
        public IIntermediateThrowEventDefinition EventDefinition { get; set; }
        /// <summary>
        /// 边界事件定义
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
