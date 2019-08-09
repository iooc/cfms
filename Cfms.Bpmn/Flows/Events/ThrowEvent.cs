using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 抛出事件的基类
    /// </summary>
    public abstract class ThrowEvent: FlowNode, IEvent
    {
        /// <summary>
        /// 输入流唯一标识的集合
        /// </summary>
        public List<Connection> Incoming { get; set; }
        /// <summary>
        /// 结束事件定义
        /// </summary>
        public IEndEventDefinition EventDefinition { get; set; }

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
        public List<SequenceFlow> Outgoing { get => throw new NotImplementedException("结束事件永不支持发出连线"); set => throw new NotImplementedException("结束事件永不支持发出连线"); }
    }
}
