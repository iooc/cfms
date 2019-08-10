using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 信号事件定义
    /// </summary>
    public class SignalEventDefinition :
        IEventDefinition,
        IStartEventDefinition,
        IIntermediateCatchEventDefinition,
        IIntermediateThrowEventDefinition,
        IEndEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 信号对象的引用
        /// </summary>
        public Signal SignalRef { get; set; }
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {
            return Task.Run(() =>
            {
                var @ref = ele.Attribute("signalRef");
                if (@ref != null)
                {
                    SignalRef = target.Where(a => a.Id == @ref.Value).FirstOrDefault() as Signal;
                }
            });
        }
    }
    /// <summary>
    /// 信号对象模型
    /// </summary>
    public class Signal : ModdleElement
    {
        /// <summary>
        /// 暂未实现
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override Task AppendLoad(XElement item, Process target)
        {
            throw new System.NotImplementedException();
        }
    }
}
