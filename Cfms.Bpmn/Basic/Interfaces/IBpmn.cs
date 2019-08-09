using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Basic.Interfaces
{
    /// <summary>
    /// BPMN 继承接口的基类
    /// </summary>
    public interface IBpmn
    {
        /// <summary>
        /// BPMN XML DOM 对象引用的别名
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// BPMN XML DOM 对象名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 图表对象类型（多声明属性，以内存换性能，减轻反射压力并提高识别准确度）
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// 补充加载剩余关联对象
        /// </summary>
        /// <param name="item">与 BPMN 对象对应的元素</param>
        /// <param name="target">生成的目标流程</param>
        /// <returns></returns>
        Task AppendLoad(XElement item, Process target);
    }
}
