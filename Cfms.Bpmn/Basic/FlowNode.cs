using Cfms.BPMN.Basic.Interfaces;

namespace Cfms.BPMN.Basic
{
    /// <summary>
    /// 流程节点流对象的基类
    /// </summary>
    public abstract class FlowNode : ModdleElement, IFlow, ILane
    {
        //public abstract bool IsAllowToSend { get; }
        //internal Subject<bool> ChangeNote { get; set; }
        /// <summary>
        /// 获取当前所属进程对象
        /// </summary>
        public IProcess ProcessRef { get; internal set; }
    }
}
