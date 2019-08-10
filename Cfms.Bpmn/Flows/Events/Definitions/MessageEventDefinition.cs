using Cfms.BPMN.Basic;
using Cfms.BPMN.Basic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Flows.Events.Definitions
{
    public class MessageEventDefinition :
        IEventDefinition,
        IStartEventDefinition,
        IIntermediateCatchEventDefinition,
        IIntermediateThrowEventDefinition,
        IEndEventDefinition,
        IBoundaryEventDefinition
    {
        /// <summary>
        /// 消息元素的引用
        /// </summary>
        public Message MessageRef { get; set; }
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        public List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }

        public Task AppendLoad(XElement ele, IProcess target)
        {
            return Task.Run(() =>
            {
                var @ref = ele.Attribute("messageRef");
                if (@ref != null)
                {
                    MessageRef = target.Where(a => a.Id == @ref.Value).FirstOrDefault() as Message;
                }
            });
        }
    }
    /// <summary>
    /// 消息对象模型
    /// </summary>
    public class Message : ModdleElement
    {
        /// <summary>
        /// 没有可附加的多余内容
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public override Task AppendLoad(XElement item, Process target)
        {
            //throw new System.NotImplementedException();
            return Task.Run(() => { });
        }
    }
}
