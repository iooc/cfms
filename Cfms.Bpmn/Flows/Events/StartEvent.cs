﻿using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using Cfms.BPMN.Connectings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 开始事件
    /// </summary>
    public class StartEvent : CatchEvent
    {

        /// <summary>
        /// 事件定义属性
        /// </summary>
        public new IStartEventDefinition EventDefinition { get; set; }
        /// <summary>
        /// 是否允许流程发送到下一步
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
        public override Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() =>
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
