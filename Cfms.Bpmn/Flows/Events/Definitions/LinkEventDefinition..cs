using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 链接事件定义
    /// </summary>
    public class LinkEventDefinition :
        IEventDefinition,
        IIntermediateCatchEventDefinition,
        IIntermediateThrowEventDefinition
    {
        /// <summary>
        /// 链接名称
        /// </summary>
        public string Name { get; set; }
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {
            return Task.Run(() =>
            {
                var name = ele.Attribute("name");
                if (name != null)
                    Name = name.Value;
            });
        }
    }
}
