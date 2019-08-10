using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 升级事件定义
    /// </summary>
    public class EscalationEventDefinition :
        IEventDefinition,
        IIntermediateThrowEventDefinition,
        IEndEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 流程外部升级节点对象的引用
        /// </summary>
        public Escalation EscalationRef { get; set; }
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {

            return Task.Run(() =>
            {
                var @ref = ele.Attribute("escalationRef");
                if (@ref != null)
                {
                    EscalationRef = target.Where(a => a.Id == @ref.Value).FirstOrDefault() as Escalation;
                }
            });
        }
    }
    /// <summary>
    /// 升级对象模型
    /// </summary>
    public class Escalation : ModdleElement
    {
        /// <summary>
        /// 预定义升级代码
        /// </summary>
        public string EscalationCode { get; set; }
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() => { });
        }
    }
}
