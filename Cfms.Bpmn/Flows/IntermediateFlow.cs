using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cfms.BPMN.Flows
{
    /// <summary>
    /// 中间流对象的基类
    /// </summary>
    public abstract class IntermediateFlow : FlowNode, IIntermediateFlow
    {
        /// <summary>
        /// 输入流连线对象引用的集合
        /// </summary>
        public List<SequenceFlow> Incoming { get; set; }
        /// <summary>
        /// 输出流连线对象引用的集合
        /// </summary>
        public List<SequenceFlow> Outgoing { get; set; }

        public override Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() =>
            {
                // 中间流对象拥有前后连接
                var ins = item.Elements("incoming");
                foreach (var @in in ins)
                {
                    if (Incoming == null)
                        Incoming = new List<SequenceFlow>();
                    Incoming.Add(target.Where(a => a.Id == @in.Value).FirstOrDefault() as SequenceFlow);
                }
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
