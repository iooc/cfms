using Cfms.BPMN.Basic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cfms.BPMN.Connectings
{
    /// <summary>
    /// 流程连接对象的基类
    /// </summary>
    public abstract class Connection : ModdleElement
    {
        /// <summary>
        /// 上级来源流节点对象
        /// </summary>
        public FlowNode SourceRef { get; set; }
        /// <summary>
        /// 发送目标流节点对象
        /// </summary>
        public FlowNode TargetRef { get; set; }
        public override Task AppendLoad(XElement item, Process target)
        {
            // 连接拥有传入流和传出流
            var sourceRef = item.Attribute("sourceRef");
            if (sourceRef != null)
                SourceRef = target.Where(a => a.Id == sourceRef.Value).FirstOrDefault() as FlowNode;

            var targetRef = item.Attribute("targetRef");
            if (targetRef != null)
                TargetRef = target.Where(a => a.Id == targetRef.Value).FirstOrDefault() as FlowNode;

            return ConditionLoad(item);
        }
        /// <summary>
        /// 加载条件表达式
        /// </summary>
        /// <returns></returns>
        protected abstract Task ConditionLoad(XElement item);
    }
}
