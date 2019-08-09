using Cfms.BPMN.Basic.Interfaces;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Basic
{
    /// <summary>
    /// 图表元素对象的基类
    /// </summary>
    public abstract class ModdleElement : IBpmn
    {
        /// <summary>
        /// BPMN XML DOM 对象引用的别名
        /// </summary>
        //[XmlAttribute]
        public string Id { get; set; }
        /// <summary>
        /// BPMN XML DOM 对象名称
        /// </summary>
        //[XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 流程图表对象类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 补充加载剩余关联对象
        /// </summary>
        /// <param name="item">与 BPMN 对象对应的元素</param>
        /// <param name="target">生成的目标流程</param>
        /// <returns></returns>
        public abstract Task AppendLoad(XElement item, Process target);
    }
}
