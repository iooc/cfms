using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 中间边界事件
    /// </summary>
    public class BoundaryEvent : CatchEvent
    {
        /// <summary>
        /// 是否可中断此活动，默认为空为可中断 true
        /// </summary>
        public bool? CancelActivity { get; set; }
        /// <summary>
        /// 边界事件锚定的元素的引用
        /// </summary>
        public Activities.Task AttachedToRef { get; set; }
        /// <summary>
        /// 边界事件定义
        /// </summary>
        public new IBoundaryEventDefinition EventDefinition { get; set; }
        /// <summary>
        /// 边界事件定义
        /// </summary>
        public override bool IsAllowToSend
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

        public override System.Threading.Tasks.Task AppendLoad(XElement item, Process target)
        {
            var attached = item.Attribute("attachedToRef");
            AttachedToRef = target.Where(a => a.Id == attached.Value).FirstOrDefault()
            as Activities.Task;

            return System.Threading.Tasks.Task.Run(() =>
            {
                // 开始事件拥有后连接
                var outs = item.Elements("outgoing");
                foreach (var @out in outs)
                {
                    if (Outgoing == null)
                        Outgoing = new List<SequenceFlow>();
                    Outgoing.Add(target.Where(a => a.Id == @out.Value).FirstOrDefault() as SequenceFlow);
                }
                ProcessRef = target;
                Executor.Init(this, item, Executor.TypesAll);
            });
        }
    }
}
