using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    /// <summary>
    /// 异常事件定义子元素（与代码异常不是一回事儿）
    /// </summary>
    public class ErrorEventDefinition :
        IEventDefinition,
        IEndEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 错误定义元素的引用
        /// </summary>
        public Error ErrorRef { get; set; }
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {
            return Task.Run(() =>
            {
                var @ref = ele.Attribute("errorRef");
                if (@ref != null)
                {
                    ErrorRef = target.Where(a => a.Id == @ref.Value).FirstOrDefault() as Error;
                }
            });
        }
    }

    public class Error : ModdleElement
    {
        /// <summary>
        /// BPMN 异常错误代码
        /// </summary>
        public string ErrorCode { get; set; }

        public override Task AppendLoad(XElement item, Process target)
        {
            return Task.Run(() => { });
        }
    }
}
