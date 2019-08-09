using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// 事件元素定义接口
    /// </summary>
    public interface IEventDefinition
    {
        /// <summary>
        /// 扩展计算对象的集合
        /// </summary>
        List<IExtensionObject<IEvent>> ExtensionCollection { get; set; }
        /// <summary>
        /// 完成附加加载计算
        /// </summary>
        /// <param name="ele">事件对象定义对应的元素</param>
        /// <param name="target">目标流程</param>
        /// <returns></returns>
        Task AppendLoad(XElement ele, IProcess target);
    }
}
