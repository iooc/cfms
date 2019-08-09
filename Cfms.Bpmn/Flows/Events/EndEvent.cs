using Cfms.BPMN.Connectings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events
{
    /// <summary>
    /// 结束事件
    /// </summary>
    public class EndEvent : ThrowEvent
    {
        public override Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() =>
            {
                // 结束事件拥有前连接
                var ins = item.Elements("incoming");
                foreach (var @in in ins)
                {
                    if (Incoming == null)
                        Incoming = new List<Connection>();
                    Incoming.Add(target.Where(a => a.Id == @in.Value).FirstOrDefault() as Connection);
                }
                ProcessRef = target;
                Executor.Init(this, item, Executor.TypesAll);
            });
        }
    }
}
