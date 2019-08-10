using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 条件事件定义表达式
    /// </summary>
    public class ConditionalEventDefinition : IEventDefinition,
        IStartEventDefinition,
        IIntermediateCatchEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }
        /// <summary>
        /// 加载条件表达式
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public Task AppendLoad(XElement ele, IProcess target)
        {
            throw new System.NotImplementedException();
        }
    }
}
