using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 补偿事件的定义
    /// </summary>
    public class CompensateEventDefinition :
        IEventDefinition,
        IIntermediateThrowEventDefinition,
        IEndEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {
            return Task.Run(() => { });
        }
    }
}
